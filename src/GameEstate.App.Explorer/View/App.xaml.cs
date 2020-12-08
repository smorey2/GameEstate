using CommandLine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

// https://www.wpf-tutorial.com/data-binding/debugging/
namespace GameEstate.Explorer.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App() => EstatePlatform.Startups.Add(OpenGLPlatform.Startup);

        //static string[] args = new string[0];
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "materials/models/npc_minions/siege1_color_psd_12a9c12b.vtex_c" };
        static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "materials/models/npc_minions/siege1.vmat_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "materials/startup_background_color_png_65ffcfa7.vtex_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "materials/startup_background.vmat_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "models/npc_minions/draft_siege_good_reference.vmesh_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "models/npc_minions/draft_siege_good.vmdl_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "models/npc_minions/draft_siege_evil_reference.vmesh_c" };
        //static string[] args = new string[] { "open", "-e", "Valve", "-u", "game:/dota/pak01_dir.vpk#Dota2", "-p", "models/npc_minions/draft_siege_evil.vmdl_c" };

        void Application_Startup(object sender, StartupEventArgs e)
        {
            //var args = e.Args;
            Parser.Default.ParseArguments<DefaultOptions, TestOptions, OpenOptions>(args)
            .MapResult(
                (DefaultOptions opts) => RunDefault(opts),
                (TestOptions opts) => RunTest(opts),
                (OpenOptions opts) => RunOpen(opts),
                errs => RunError(errs));
        }

        #region Options

        [Verb("default", true, HelpText = "Default action.")]
        class DefaultOptions { }

        [Verb("test", HelpText = "Test fixture.")]
        class TestOptions { }

        [Verb("open", HelpText = "Extract files contents to folder.")]
        class OpenOptions
        {
            [Option('e', "estate", HelpText = "Estate", Required = true)]
            public string Estate { get; set; }

            [Option('u', "uri", HelpText = "Pak file to be opened", Required = true)]
            public Uri Uri { get; set; }

            [Option('p', "path", HelpText = "optional file to be opened")]
            public string Path { get; set; }
        }

        #endregion

        static int RunDefault(DefaultOptions opts)
        {
            new MainWindow().Show();
            return 0;
        }

        static int RunTest(TestOptions opts)
        {
            var wnd = new MainWindow();
            wnd.Show();
            return 0;
        }

        static int RunOpen(OpenOptions opts)
        {
            var estate = EstateManager.GetEstate(opts.Estate);
            var wnd = new MainWindow(false);
            wnd.Open(estate, new[] { opts.Uri }, opts.Path);
            wnd.Show();
            return 0;
        }

        static int RunError(IEnumerable<Error> errs)
        {
            MessageBox.Show("Errors: \n\n" + errs.First());
            Current.Shutdown(1);
            return 1;
        }
    }
}
