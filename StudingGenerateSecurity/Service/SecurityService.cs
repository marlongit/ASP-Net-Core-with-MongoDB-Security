using StudingGenerateSecurity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudingGenerateSecurity.Service
{
    public sealed class SecurityService
    {
        private SecurityService()
        {
        }

        private static SecurityService _instance;

        public static SecurityService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (typeof(SecurityService))
                    {
                        _instance = new SecurityService();
                    }
                }

                return _instance;
            }
        }

        public string NewPassword(int lenght, bool whithCaracterEspecial = true)
        {
            string password = string.Empty;
            string validade = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890@#$%&*!+-=";
            string validatorSemCaracter = "abcdefghijklmnozABCDEFGHIJKLMNOZ1234567890";

            try
            {
                StringBuilder strbld = new StringBuilder(100);
                Random random = new Random();
                if (whithCaracterEspecial)
                {
                    while (0 < lenght--)
                    {
                        strbld.Append(validade[random.Next(validade.Length)]);
                    }
                }
                else
                {
                    while (0 < lenght--)
                    {
                        strbld.Append(validatorSemCaracter[random.Next(validatorSemCaracter.Length)]);
                    }
                }
                password = strbld.ToString();

                return password;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Create(SecurityEntity entity)
        {
            try
            {
                entity.OId = Guid.NewGuid();
                entity.DateCreate = DateTime.Now;
                ConnectionService.Instance.Create<SecurityEntity>(entity);

                return "The password was save with successfully !";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update(SecurityEntity entity)
        {
            try
            {
                return ConnectionService.Instance.Update<SecurityEntity>(x => x.OId == entity.OId, entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IList<SecurityEntity> ListAll()
        {
            try
            {
                var lista = ConnectionService.Instance.ListAll<SecurityEntity>();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<SecurityEntity> ListAllByFilter(SecurityEntity entity)
        {
            try
            {
                var lista = ConnectionService.Instance.ListAllByFilter<SecurityEntity>(x => x.OId == entity.OId);
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
