﻿using System.Collections.Generic;
using System.Data.SqlClient;

namespace SqlMapper.DataMapper
{
    public interface IDataMapper<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll(); // Devolve todos os elementos da tabela correspondente
        void Update(T val); // Actualiza a linha que tem PK igual à propriedade PK de val (ler cap. Requisitos)
        void Delete(T val); // Apaga a linha com PK igual à propriedade PK de val
        int Insert(T val); // Insere uma nova linha com os valores de val e actualiza val com a PK devolvida

        void SetTransaction(SqlTransaction transaction);

        T Create(SqlDataReader dr);
    }
}