﻿using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal sealed class ReactionsController<Item> where Item : class
    {
        private struct ReactionAguardada
        {
            public DiscordEmoji Emoji { get; set; }
            public DiscordUser Autor { get; set; }
            public DiscordMessage Msg { get; set; }
            public Tuple<MethodInfo, object> FunctionResultante { get; set; }
            public DateTime AdicionadoEm { get; set; }
            public object[] Args { get; set; }
        }

        private static List<ReactionAguardada> BufferReacoes = new List<ReactionAguardada>();
        private Item Contexto;

        internal ReactionsController(Item contexto)
        {
            Contexto = contexto;
            dynamic conversionitem = Convert.ChangeType(contexto, typeof(Item));
            DiscordClient cliente = conversionitem.Client;
            cliente.MessageReactionAdded += ClienteDiscord_MessageReactionAdded;
            cliente.MessageReactionRemoved += Cliente_MessageReactionRemoved;
        }

        private Task Cliente_MessageReactionRemoved(MessageReactionRemoveEventArgs e)
        {
            new Thread(Action).Start(new object[] { e.User, e.Message, e.Emoji, e.Client });
            return Task.CompletedTask;
        }


        internal void AddReactionEvent(DiscordMessage msg, Tuple<MethodInfo, object> exec, DiscordEmoji emoji = null, DiscordUser autor = null, params object[] args)
        {
            ReactionAguardada reacao = new ReactionAguardada
            {
                Emoji = emoji,
                Msg = msg,
                Autor = autor,
                FunctionResultante = exec,
                AdicionadoEm = DateTime.Now,
                Args = args
            };
            BufferReacoes.Add(reacao);
        }

        internal Tuple<MethodInfo, object> ConvertToMethodInfo<ArgType>(Func<Item, ArgType, Task> funcao) where ArgType : class
        {
            return Tuple.Create(funcao.Method, funcao.Target);
        }

        internal Tuple<MethodInfo, object> ConvertToMethodInfo(Func<Item, Task> funcao)
        {
            return Tuple.Create(funcao.Method, funcao.Target);
        }

        private Task ClienteDiscord_MessageReactionAdded(MessageReactionAddEventArgs e)
        {
            new Thread(Action).Start(new object[] { e.User, e.Message, e.Emoji, e.Client });
            return Task.CompletedTask;
        }

        private void Action(object args)
        {
            try
            {
                BufferReacoes.RemoveAll(x => x.AdicionadoEm >= x.AdicionadoEm.AddMinutes(5));
                int index = -1;
                for (int i = 0; i < BufferReacoes.Count; i++)
                {
                    if (BufferReacoes[i].Msg == (DiscordMessage)((object[])args)[1])
                    {
                        bool validado = true;
                        if (BufferReacoes[i].Autor != null && BufferReacoes[i].Autor != (DiscordUser)((object[])args)[0])
                            validado = false;
                        if (BufferReacoes[i].Emoji != null && BufferReacoes[i].Emoji != (DiscordEmoji)((object[])args)[2])
                            validado = false;
                        if (validado)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (index >= 0)
                {
                    List<object> obj = new List<object>();
                    obj.Add(Contexto);
                    obj.AddRange(BufferReacoes[index].Args);
                    BufferReacoes[index].FunctionResultante.Item1.Invoke(BufferReacoes[index].FunctionResultante.Item2, obj.ToArray());
                    BufferReacoes.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
                ((BaseDiscordClient)((object[])args)[3]).DebugLogger.LogMessage(LogLevel.Info, "Kurosawa Dia - Event", ex.Message, DateTime.Now);
            }
        }


    }
}
