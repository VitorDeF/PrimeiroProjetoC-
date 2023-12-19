using AbaClientes;

namespace Programa {
    class Programa {
        static void Main(string[] args) {
            try {
                DicionarioClientes dc = new DicionarioClientes();
                DicionarioClientes.cadastraCliente(new Cliente("14261330652", "Vítor", "vitor@email.com"));
                DicionarioClientes.cadastraCliente(new Cliente("11144477735", "Vitor", "vitor@email.com"));
                foreach (var cliente in DicionarioClientes.Dicionario) {
                    Console.WriteLine(cliente.Value);
                }
                Cliente copia = dc.obtemCliente("11144477735");
                Console.WriteLine(copia);
                DicionarioClientes.cadastraCliente(copia);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}