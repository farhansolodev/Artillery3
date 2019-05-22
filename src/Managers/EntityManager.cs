﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery
{
    public interface IEntityManager
    {
        void AddEntity(Entity e);
        void RemoveEntity(Entity e);
        void Update();
        void Draw();
        bool HasEntity(Entity e);
        bool HasEntity(string name);
    }
    public class EntityManager : IEntityManager
    {
        #region Fields
        A3Data _a3Data;

        #endregion

        #region Constructor
        public EntityManager(A3Data a3Data)
        {
            

        }
        #endregion

        #region Methods

        public void Update()
        {
            foreach(Entity e in _entities)
            {
                if(e.Enabled)
                    e.Update();
                if (e.ToBeRemoved)
                    RemoveEntity(e);
            }

            foreach (Entity e in _entitiesToAdd)
            {
                if (e.Enabled)
                    e.Update();
                _entities.Add(e);
            }

            foreach(Entity e in _entitiesToRemove)
            {
                _entities.Remove(e);
            }

            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        }

        public void Draw()
        {
            foreach (Entity e in _entities)
            {
                e.Draw();
            }
        }

        public void AddEntity(Entity e)
        {
            _entitiesToAdd.Add(e);
        }

        public bool HasEntity(Entity e)
        {
            throw new NotImplementedException();
        }

        public bool HasEntity(string name)
        {
            throw new NotImplementedException();
        }

        public void RemoveEntity(Entity e)
        {
            _entitiesToRemove.Add(e);
        }

        #endregion

        #region Properties
        public List<Entity> Entities { get => _entities; }
        #endregion

    }
}
