﻿using SuplementosFGFit_Back.Repositorios.Repositorio;
using SuplementosFGFit_Back.Models;

namespace SuplementosFGFit_Back.Repositorios.IRepositorio
{
    public interface IFormaEnvioRepositorio : IRepositorio<FormasEnvio>
    {
        Task<FormasEnvio> Actualizar(FormasEnvio form);
    }
}
