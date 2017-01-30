using Lapiwe.Common.Domain;
using Lapiwe.GMS.FrontEnd.Entities;
using System;
using System.Collections.Generic;

namespace Lapiwe.GMS.FrontEnd.DAL
{
    public interface ISimpleRepository
    {
        TEntity LazyLoadFind<TEntity>(Guid guid) where TEntity : DomainEntity;
        IEnumerable<TEntity> LazyLoadAll<TEntity>() where TEntity : DomainEntity;
        void Add<TEntity>(TEntity entity) where TEntity : DomainEntity;
        OnderhoudsOpdracht VindOnderhoudsOpdracht(Guid guid);
        IEnumerable<OnderhoudsOpdracht> AlleOnderhoudsOpdrachten();
    }
}