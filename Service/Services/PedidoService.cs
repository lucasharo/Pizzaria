using AutoMapper;
using Infra.Interfaces;
using Service.Interfaces;
using System;
using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using AutoMapper.Internal;

namespace Service.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PedidoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string CadastrarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                ValidarPedido(pedidoDTO);

                if (pedidoDTO.IdUsuario.HasValue && pedidoDTO.IdUsuario.Value > 0)
                {
                    var usuario = _unitOfWork.UsuarioRepository.GetUsuarioById(pedidoDTO.IdUsuario.Value);

                    pedidoDTO.IdUsuario = usuario.Id;
                    pedidoDTO.Nome = usuario.Nome + " " + usuario.Sobrenome;

                    pedidoDTO.Endereco = _unitOfWork.UsuarioRepository.GetEnderecoUsuarioById(pedidoDTO.IdUsuario.Value);
                }

                CalcularValores(pedidoDTO);

                var pedido = new Pedido
                {
                    IdUsuario = pedidoDTO.IdUsuario,
                    Nome = pedidoDTO.Nome,
                    Valor = pedidoDTO.Valor,
                    CEP = pedidoDTO.Endereco.CEP,
                    Logradouro = pedidoDTO.Endereco.Logradouro,
                    Numero = pedidoDTO.Endereco.Numero,
                    Complemento = pedidoDTO.Endereco.Complemento,
                    Bairro = pedidoDTO.Endereco.Bairro,
                    Cidade = pedidoDTO.Endereco.Cidade
                };

                _unitOfWork.PedidoRepository.InserirPedido(pedido);

                foreach (var pizzaDTO in pedidoDTO.Pizzas)
                {
                    var pizza = new Pizza
                    {
                        IdPedido = pedido.Id,
                        Valor = pizzaDTO.Valor
                    };

                    _unitOfWork.PedidoRepository.InserirPizza(pizza);

                    foreach (var sabor in pizzaDTO.Sabores)
                    {
                        var pizzaSabor = new Pizza_Sabor
                        {
                            IdPizza = pizza.Id,
                            IdSabor = sabor.Id
                        };

                        _unitOfWork.PedidoRepository.InserirPizzaSabor(pizzaSabor);
                    }
                }

                _unitOfWork.Commit();

                return "Pedido realizado com sucesso!";
            } catch (AppException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cadastrar pedido", ex);
            }
        }

        private void CalcularValores(PedidoDTO pedidoDTO)
        {
            pedidoDTO.Valor = 0;

            var listaSabores = _unitOfWork.PedidoRepository.ListarSabores();

            foreach (var pizzaDTO in pedidoDTO.Pizzas)
            {
                var saboresValor = pizzaDTO.Sabores.Join(listaSabores,
                    a => a.Id,
                    b => b.Id,
                    (a, b) => b);

                decimal valor = 0;

                if (saboresValor.Count() > 1)
                {
                    foreach (var sabor in saboresValor)
                    {
                        valor += sabor.Preco / 2;
                    }
                }
                else
                {
                    valor = saboresValor.FirstOrDefault().Preco;
                }

                pizzaDTO.Valor = valor;

                pedidoDTO.Valor += valor;
            }
        }

        private void ValidarPedido(PedidoDTO pedido)
        {
            if (pedido.Endereco != null)
            {
                if (pedido.Endereco.CEP == null)
                {
                    throw new AppException("Favor informar o CEP");
                }
                else if (pedido.Endereco.Logradouro == null)
                {
                    throw new AppException("Favor informar o endereço");
                }
                else if (pedido.Endereco.Numero == null)
                {
                    throw new AppException("Favor informar o número do endereço");
                }
                else if (pedido.Endereco.Bairro == null)
                {
                    throw new AppException("Favor informar o bairro");
                }
                else if (pedido.Endereco.Cidade == null)
                {
                    throw new AppException("Favor informar a cidade");
                }
            }

            if (pedido.Pizzas.Count() < 0)
            {
                throw new AppException("Favor informar ao menos uma pizza");
            }
            else if (pedido.Pizzas.Count() > 10)
            {
                throw new AppException("O pedido não pode conter mais que 10 pizzas");
            }
            else
            {
                foreach (var pizza in pedido.Pizzas)
                {
                    if (pizza.Sabores.Count() > 2)
                    {
                        throw new AppException("A pizza não pode conter mais que 2 sabores");
                    }
                    else if (pizza.Sabores.Count() < 0)
                    {
                        throw new AppException("Favor informar ao menos um sabor");
                    }
                }
            }
        }
    }
}