using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Utils
{
    public class Sessao
    {
        private const string Nome_Sessao = "CarrinhoID";
        public static string RetornarCarrinhoId()
        {
            if(HttpContext.Current.Session[Nome_Sessao] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session[Nome_Sessao] = guid.ToString();
            }

            return HttpContext.Current.Session[Nome_Sessao].ToString();
        }
    }
}