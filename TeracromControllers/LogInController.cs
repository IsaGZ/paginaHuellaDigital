using DPUruNet;
using Newtonsoft.Json;
using TeracromModels;

namespace TeracromControllers
{
    public class LogInController
    {
        public async Task<Dictionary<int, Fmd>> obtenerUsuariosHuella()
        {
            Dictionary<int, DPUruNet.Fmd> fmds = new Dictionary<int, DPUruNet.Fmd>();

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "https://localhost:44315/Huella";
                var huellas = await ObtenerHuellasDesdeAPI(apiUrl); // Obtener huellas desde la API

                if (huellas != null)
                {
                    foreach (var huella in huellas)
                    {
                        try
                        {
                            // Crear un objeto FMD desde los datos de la API
                            Fmd fmd = new Fmd(huella.fmdBytes, huella.fmdFormat, huella.fmdVersion);

                            // Agregar el idUsuario y el FMD al diccionario
                            fmds.Add(huella.idUsuario, fmd);

                            // Opcionalmente imprimir los datos de la huella
                            Console.WriteLine($"ID Usuario: {huella.idUsuario}, Formato: {fmd.Format}, Versión: {fmd.Version}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al procesar la huella para el usuario {huella.idUsuario}: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se obtuvieron huellas desde la API.");
                }
            }

            return fmds; // Retorna el diccionario con los FMDs
        }
        // Este método obtiene las huellas desde la API
        public static async Task<List<HuellaUsuario>> ObtenerHuellasDesdeAPI(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Realizar una solicitud GET a la API
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();

                        // Deserializar los datos JSON recibidos desde la API
                        List<HuellaUsuario> huellas = JsonConvert.DeserializeObject<List<HuellaUsuario>>(jsonResult);
                        return huellas;
                    }
                    else
                    {
                        Console.WriteLine($"Error al obtener datos de la API: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Excepción al obtener datos de la API: {e.Message}");
                    return null;
                }
            }
        }
    }
    
}
