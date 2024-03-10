using System.Runtime.InteropServices;
using CommandLine;
using CommandLine.Text;

namespace GBattle
{
    internal class Program
    {
        class Options
        {
            [Option("rebelle", Required = true, HelpText = "Nombre de rebelles")]
            public string Rebelle { get; set; }

            [Option("empire", Required = true, HelpText = "Nombre d'impérialistes")]
            public string Empire { get; set; }
        }

        static void Main(string[] args)
        {
            int sizeRebelle = 0;
            int sizeEmpire = 0;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => 
                {
                    try { sizeRebelle = int.Parse(o.Rebelle); }
                    catch { throw new Exception("les tailles des armées doivent être un nombre strictement supérieur à 0."); }
                    try { sizeEmpire = int.Parse(o.Empire); }
                    catch { throw new Exception("les tailles des armées doivent être un nombre strictement supérieur à 0."); }
                });
            if (sizeRebelle <= 0 | sizeEmpire <= 0) { throw new Exception("les tailles des armées doivent être un nombre strictement supérieur à 0."); }
            Game game = new Game(sizeRebelle, sizeEmpire);
            game.run();
        }
    }
}
