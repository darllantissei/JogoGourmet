﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoGourmet.Interfaces
{
    public interface IGourmet
    {
        DialogResult RespostaUsuario {get; set;}

        void IniciarPergunta();
        
    }
}
