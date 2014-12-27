using FluentSharp.CassiniDev;
using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.NUnit;
using FluentSharp.Web35;
using NUnit.Framework;

namespace FluentSharp.CassiniDev.NUnit
{
    public class NUnitTests_Cassini : NUnitTests
    {
        public API_Cassini apiCassini;
        public string      webRoot;
        public int         port;

        [TestFixtureSetUp]        
        public void start()
        {       
            apiCassini.assert_Null();
            webRoot   .assert_Folder_Not_Exists();
            port      .assert_Default();

            apiCassini = new API_Cassini();
            webRoot    = apiCassini.webRoot();
            port       = apiCassini.port();
                        

            webRoot   .assert_Folder_Exists();
            port      .tcpClient().assert_Null();
            apiCassini.start();
            port      .tcpClient().assert_Not_Null();
        }
        
        [TestFixtureTearDown]
        public void stop()
        {
            port      .tcpClient().assert_Not_Null();
            apiCassini.stop();
            port      .tcpClient().assert_Null();                        
              
            for(var i=0; i<10;i++)
                if(Files.deleteFolder(webRoot, true))
                    break;
                "wating for being able to delete folder".info();
                100.sleep();
            //webRoot.folder_Wait_For_Deleted();
            //   webRoot.parentFolder().startProcess();
            webRoot.assert_Folder_Not_Exists();
            // 
        }        
    }
}