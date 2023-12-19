using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbaClientes {
    public class DicionarioClientes {
        // Padrao Singleton
        static DicionarioClientes() { }
        static DicionarioClientes instancia;
        public static DicionarioClientes getInstancia() {
            if (instancia == null) {
                instancia = new DicionarioClientes();
            }
            return instancia;
        }
        static Dictionary<string, Cliente> dicionario = new Dictionary<string, Cliente>();
        public static Dictionary<string, Cliente> Dicionario {
            get { return dicionario; }
        }
        //Confere se o cliente já está cadastrado
        public static bool clienteCadastrado(Cliente novo) {
            if (dicionario.ContainsKey(novo.ID))
                return true;
            return false;
        }
        //Confere se o CPF ou o Nome do cliente já estão cadastrados
        public static bool cpfOuNomeCadastrado(Cliente novo) {
            foreach (var cliente in dicionario)
                if (cliente.Value.Nome == novo.Nome || cliente.Value.CPF == novo.CPF)
                    return true;
            return false;
        }
        //Realiza o cadastro de cliente
        public static void cadastraCliente(Cliente novo) {
            if (clienteCadastrado(novo))
                throw new Exception("Cliente já cadastrado.");
            else if (cpfOuNomeCadastrado(novo))
                throw new Exception("Dados já encontrados no banco de dados");
            dicionario.Add(novo.ID, novo);
        }
        public Cliente obtemCliente(string cpf) {
            cpf = Cliente.formataCPF(cpf);
            foreach (var c in dicionario) {
                if (c.Value.CPF == cpf)
                    return c.Value;
            }
            throw new Exception("Cliente não encontrado.");
        }
    }


    public class Cliente {
        private string nome, email, cpf, id;
        public string CPF {
            get { return cpf; }
            set { cpf = value; }
        }
        public string Nome {
            get { return nome; }
            set { nome = value; }
        }
        public string Email {
            get { return email; }
            set { email = value; }
        }
        public string ID {
            get => id;
        }
        public Cliente(string cpf, string nome, string email) {
            CPF = formataCPF(cpf);
            Nome = nome;
            Email = email;
            id = CPF + Nome;
        }
        public static string formataCPF(string cpf) {
            if (IsCpf(cpf))
                return Convert.ToInt64(cpf).ToString(@"000\.000\.000\-00");
            else {
                throw new Exception("Fora do formato.");
            }
        }

        public override string ToString() {
            return $"CPF: {CPF}, Nome: {Nome}, Email: {Email}";
        }
        public static bool IsCpf(string cpf) {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
