using EventDelegateExample;
using FirstLibrary.Testing;
using Formulatrix.License;
using MethodExtensionExample;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic; // List
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace FirstApp
{
  class Program
  {
    //[Conditional("DEBUG")]
    static void Main( string[] args )
    {
      var watch = new System.Diagnostics.Stopwatch();
      watch.Start();

      try
      {
        // 1. Value or Reference
        //ExCopyByValue();
        //ExCopyByReference();

        // 2. Nullable
        //NullableExample();

        // 3. String
        //ExString();

        // 4. Array
        //ExArray();

        // 5. Paramaters pass by ref
        //int passRef = 5;
        //ExPassByref(ref passRef); // Value type needs ref keyword to pass by ref
        //var test = new ParentClass( "Yanto" ); // Referenced type auto pass by ref
        //ExPassByRefNoKeyword( test );
        //Console.WriteLine( test.Name );

        // 6. Arguments in function
        //PrintArguments(x: 21);

        // 7. Attributes
        //Console.WriteLine("asdad");

        // 8. Properties
        //ExProperties();

        // 9. Generics (similar to template in C++)

        // 10. Loop
        //ExLoop();

        // 11. External library
        //ExLib();
        //ExLib("Test overloading");

        // 12. Object intializers
        //var newCar = new Car<int>{hidden = 2400, number:4};

        // 13. Partial class, must be on the same assembly
        //var newPartialClass = new PartialClass();

        // 14. Upcast n Downcast
        //ExInheritanceCasting();

        // 15. virtual, override, new (overloading, overriding, hiding)
        //ExVirtOverHide();

        //16. Interface and Abstract
        //ExInterfaceAbstract();

        //17. Threading, Some executions run concurently
        //ExThreading();

        //18. Async await
        //ExAsyncAwait();
        //Console.ReadKey();

        //19. Async await with Progress
        //ExAsyncAwaitProgress();
        //Console.ReadKey();

        //20. http request
        //DelegateTask task = ExHttpRequest;
        //task(); //version 1
        //task.Invoke(); //version 2
        //Console.ReadKey();

        //DelegateVoid t = WriteY;
        //21. Delegate
        //t();

        //22. Event and Delegate
        //ExEventDelegate();

        //23. LINQ
        //ExLINQ();
        //Console.ReadKey();

        //24. Method Extension Class
        //ExMethodExtensionClass();

        //25. Nameof syntax
        //ExNameOf();

        //26. Parsing Double
        //ExDoubleParsing();

        //27. File and Directory Path
        //ExFileDirectoryPath();

        //28. Get Class from String
        //ExGetClassFromString();

        // 29. Enum 
        //ExTestEnumFunc();

        // 30. Check Out
        //ExCheckOutParam();

        //31. Reverse search
        //ExReverseSearchList();

        //32. Timer
        //ExTimer();

        //33. Multiple Assignment
        //ExMultipleAssignment();

        //34. Switch case no break
        //ExSwitchCaseNobreak();

        //35. Deserialize Class in Class
        //ExDeserializeNestingClass();

        //36. Bitmask
        //ExBitmask();

        //37. Read Version Assembly
        //ExReadVersionAssembly();

        //38. Create Long Directory
        //ExCreateLongDirectory();

        //39. Xml read, edit, write File 
        //ExXmlFile();

        //40. SortListVersion
        //ExSortListVersion();

        //41. Regex 
        // ExRegex();

        //42. Sftp modify file
        //ExModifyFileRemote();

        //43. ssh client command
        //ExSshClient();

        //44. Convert CRLF to LF
        //ExConverCLRF();

        //45. Json edit and write
        //ExJsonEditWrite();

        //46. DateTime formatter
        // ExDateTimeFormat();

        //47. Reading value
        //ExSignalStartDone();

        //50. Monitor Wait Pulse
        //var monitorPulseObj = new object();
        //ExMonitorWaitPulse( monitorPulseObj );

        //51. License FMLX
        //ExLicenseTest();

        //52. Open zip and read version
        //ExOpenZipAndReadVersion();

        //53. Check ip address
        //ExCheckIpAddress();

        //54. Check file info
        //ExFileInfo();

        //55.NamedPipeClientStream
        //ExPipeCom();

        //56. TestIf
        //ExIf();

        //57. TestPostSharper
        //Console.WriteLine( ExPostSharper() );

        //58. Send email
        //SendEmail();

        //59. Encrypt Decrypt
        //ExEncryptDecrypt();

        //60. Test Order Ui File
        // ExTestOrderUiFile();

        //61. Test order debs file
        ExTestOrderDebsFile();
      }
      catch( Exception e )
      {
        Console.WriteLine( "Error: " + e.Message );
      }

      watch.Stop();

      Console.WriteLine( $"\nTotal execution time: {watch.ElapsedMilliseconds}ms" );
    }
    private static void ExTestOrderDebsFile()
    {
      string dirpath = Path.Combine( AppContext.BaseDirectory, "network-config" );

      List<FileInfo> listDebs = Directory.GetFiles( dirpath, "*.deb", SearchOption.TopDirectoryOnly )
        .Select( ( filename ) => new FileInfo( filename ) )
        .OrderBy( ( fileInfo ) => fileInfo.Name.Split( '-' ).First() )
        .ToList();

      for( var i = 0; i < listDebs.Count; i++ )
      {
        Console.WriteLine( listDebs[ i ].FullName );
      }
    }
    private static void ExTestOrderUiFile()
    {
      string dirPath = Path.Combine( AppContext.BaseDirectory, "TestFiles", "ui" );

      FileInfo currentLog = new FileInfo( Path.Combine( dirPath, "log.txt" ) );
      List<FileInfo> listResult = new List<FileInfo>()
      {
        currentLog
      };

      List<FileInfo> listTemp = Directory.GetFiles( dirPath, "log.*.txt", SearchOption.TopDirectoryOnly )
        .Select( ( file ) => new FileInfo( file ) )
        .OrderBy( ( fileInfo ) => fileInfo.Name )
        .Reverse()
        .ToList();

      if( !IsFileHasKeyword( currentLog ) && listTemp.Count != 0 )
      {
        listResult.Add( listTemp.First() );
      }

      foreach( var file in listResult )
      {
        Console.WriteLine( file.Name );
      }
    }

    private const string KEYWORD = "LOG UI START";
    private static bool IsFileHasKeyword( FileInfo logfile )
    {
      using( var fileStream = logfile.OpenRead() )
      {
        using( var reader = new StreamReader( fileStream, Encoding.UTF8 ) )
        {
          return reader.ReadLine().Contains( KEYWORD );
        }
      }
    }
    private static long GetDirectorySize( List<FileInfo> listLogFileInfo )
    {
      long size = 0;

      foreach( var file in listLogFileInfo )
      {
        size += file.Length;
      }

      return size;
    }
    private static void ExEncryptDecrypt()
    {
      string original = "f45tfmlxf@iry";
      Console.WriteLine( "Original: " + original );
      Console.WriteLine( "ASCII ENCRYPTION" );
      string encrypted = EncryptString( original );
      Console.WriteLine( encrypted );
      Console.WriteLine( DecryptString( encrypted ) );
      Console.WriteLine( "AES ENCRYPTION" );
      encrypted = Encrypt( original );
      Console.WriteLine( encrypted );
      try
      {
        Console.WriteLine( Decrypt( encrypted ) );
      }
      catch( Exception )
      {
        Console.WriteLine( "Failed to decrypt" );
      }
    }

    private static string DecryptString( string encrString )
    {
      byte[] b;
      string decrypted;
      try
      {
        b = Convert.FromBase64String( encrString );
        decrypted = Encoding.ASCII.GetString( b );
      }
      catch( FormatException fe )
      {
        decrypted = "";
      }
      return decrypted;
    }

    private static string EncryptString( string strEncrypted )
    {
      byte[] b = Encoding.ASCII.GetBytes( strEncrypted );
      string encrypted = Convert.ToBase64String( b );
      return encrypted;
    }

    private static string Encrypt( string encryptString )
    {
      string EncryptionKey = "f45tfmlxf@iry";
      byte[] clearBytes = Encoding.Unicode.GetBytes( encryptString );
      using( Aes encryptor = Aes.Create() )
      {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes( EncryptionKey,
          new byte[]
          {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
          } );
        encryptor.Key = pdb.GetBytes( 32 );
        encryptor.IV = pdb.GetBytes( 16 );
        using( MemoryStream ms = new MemoryStream() )
        {
          using( CryptoStream cs = new CryptoStream( ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write ) )
          {
            cs.Write( clearBytes, 0, clearBytes.Length );
            cs.Close();
          }
          encryptString = Convert.ToBase64String( ms.ToArray() );
        }
      }
      return encryptString;
    }

    private static string Decrypt( string cipherText )
    {
      string EncryptionKey = "f45tfmlxf@iry";
      cipherText = cipherText.Replace( " ", "+" );
      byte[] cipherBytes = Convert.FromBase64String( cipherText );
      using( Aes encryptor = Aes.Create() )
      {
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes( EncryptionKey,
          new byte[]
          {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
          } );
        encryptor.Key = pdb.GetBytes( 32 );
        encryptor.IV = pdb.GetBytes( 16 );
        using( MemoryStream ms = new MemoryStream() )
        {
          using( CryptoStream cs = new CryptoStream( ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write ) )
          {
            cs.Write( cipherBytes, 0, cipherBytes.Length );
            cs.Close();
          }
          cipherText = Encoding.Unicode.GetString( ms.ToArray() );
        }
      }
      return cipherText;
    }

    private static void SendEmail()
    {
      try
      {
        MailMessage message = new MailMessage();
        message.From = new MailAddress( "zdayvidz@gmail.com" );
        message.To.Add( new MailAddress( "zdayvidz@gmail.com" ) );
        message.Subject = "Test";
        message.Body = "Body content";
        message.Attachments.Add( new Attachment( Path.Combine( AppContext.BaseDirectory, "Files", "Temporary_Valid.xml" ) ) );
        //message.IsBodyHtml = true; //to make message body as html  
        //message.Body = htmlString;
        using( SmtpClient smtp = new SmtpClient() )
        {
          smtp.Port = 587;
          smtp.Host = "smtp.gmail.com"; //for gmail host  
          smtp.EnableSsl = true;
          smtp.UseDefaultCredentials = false;
          smtp.Credentials = new NetworkCredential( "zdayvidz@gmail.com", "Yondaimebt12" );
          smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
          smtp.Send( message );
        }
      }
      catch( Exception e )
      {
        Console.WriteLine( e.Message );
      }
    }

    private static void ExIf()
    {
      int a = 90;
      if( a > 80 )
        Console.WriteLine( "more than 80" );
      else if( a > 70 )
        Console.WriteLine( "more than 70" );
      else
        Console.WriteLine( "else" );
    }

    private static void ExPipeCom()
    {
      using( NamedPipeClientStream pipeClient = new NamedPipeClientStream( ".", "testpipe", PipeDirection.In ) )
      {
        // Connect to the pipe or wait until the pipe is available.
        Console.Write( "Attempting to connect to pipe..." );
        pipeClient.Connect();

        Console.WriteLine( "Connected to pipe." );
        Console.WriteLine( "There are currently {0} pipe server instances open.",
          pipeClient.NumberOfServerInstances );
        using( StreamReader sr = new StreamReader( pipeClient ) )
        {
          // Display the read text to the console
          string temp;
          while (( temp = sr.ReadLine() ) != null)
          {
            Console.WriteLine( "Received from server: {0}", temp );
          }
        }
      }
    }

    private static void ExFileInfo()
    {
      FileInfo fileInfo = new FileInfo( "/opt/formulatrix/filename.zip" );
      Console.WriteLine( fileInfo.Name );
      Console.WriteLine( fileInfo.Directory.FullName );
    }

    private static void ExCheckIpAddress()
    {
      string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
      Console.WriteLine( hostName );
      // Get the IP  
      foreach( IPAddress myIP in Dns.GetHostEntry( hostName ).AddressList )
      {
        if( myIP.AddressFamily == AddressFamily.InterNetwork )
          Console.WriteLine( "My IP Address is :" + myIP.ToString() );
      }
    }

    private static void ExOpenZipAndReadVersion()
    {
      string baseZip = Path.Combine( "ZipFiles", "FastInstrument.zip" );
      File.Create( Path.Combine( "ZipFiles", "test.txt" ) );
      using( ZipArchive zip = ZipFile.OpenRead( baseZip ) )
      {
        var assembly = zip.Entries.Where( ( entry ) => entry.Name == "FastInstrument.dll" ).First();
        if( File.Exists( "FastInstrument.dll" ) )
          File.Delete( "FastInstrument.dll" );

        assembly.ExtractToFile( "FastInstrument.dll" );
        //assembly.ExtractToFile( "ZipFiles" );
        Console.WriteLine( AssemblyName.GetAssemblyName( Path.Combine( "FastInstrument.dll" ) ).Version.ToString() );
      }
    }

    private static void ExLicenseTest()
    {
      // license file
      foreach( var item in Directory.GetFiles( Path.Combine( AppContext.BaseDirectory, "Files" ), "*", SearchOption.AllDirectories ) )
      {
        FileInfo licenseFileInfo = new FileInfo( item );
        Console.WriteLine( licenseFileInfo.FullName );
        string instrumentName = "FAST-205";
        string productName = "FAST";
        string licenseKey = "f45tfmlxf@iry";

        LicenseManagerConfig licenseConfig = new LicenseManagerConfig( licenseFileInfo, instrumentName, productName, licenseKey );
        InstrumentLicenseManager licenseManager = new InstrumentLicenseManager( licenseConfig );

        try
        {
          licenseManager.Initialize();
        }
        catch( Exception e )
        {
          Console.WriteLine( e.Message );
        }

        Console.WriteLine( $"Status: {licenseManager.GetLicenseStatus().ToString()}" );
      }


      //switch( licenseManager.GetLicenseStatus() )
      //{
      //  case LicenseStatus.NoLicense:
      //    //licenseManager.CreateTrialLicense( "ZugZug" );
      //    break;
      //  case LicenseStatus.Error:
      //    break;
      //  case LicenseStatus.Trial:
      //    Console.WriteLine( licenseManager.GetClientInfo().Name );
      //    Console.WriteLine( licenseManager.HasTrialLicenseActivated() );
      //    break;
      //  case LicenseStatus.TrialExpired:
      //    break;
      //  case LicenseStatus.Temporary:
      //    break;
      //  case LicenseStatus.TemporaryExpired:
      //    break;
      //  case LicenseStatus.Permanent:
      //    break;
      //  case LicenseStatus.PermanentExpired:
      //    break;
      //  default:
      //    break;
      //}

      //GetLicenseManager();
    }

    private static void ExMonitorWaitPulse( object obj )
    {
      Console.WriteLine( "Boss prepare work" );
      Thread boss = new Thread( () => { } );
      Thread employee = new Thread( () => { } );
    }

    private static bool _status = false;

    private static (int min, int max) GetWeight()
    {
      Random random = new Random();
      return ( random.Next( -20000, 20000 ), random.Next( -20000, 20000 ) );
    }

    private static void ExSignalStartDone()
    {
      Task<int> thread = new Task<int>( () =>
      {
        _status = true;
        int max = 0;

        while (_status)
        {
          Thread.Sleep( 1000 );
          var weight = GetWeight();

          Console.WriteLine( $"{weight.min}, {weight.max}, {DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}" );

          if( max < Math.Max( Math.Abs( weight.min ), Math.Abs( weight.max ) ) )
            max = Math.Max( Math.Abs( weight.min ), Math.Abs( weight.max ) );
        }

        return max;
      } );

      thread.Start();

      StopCounting();

      thread.Wait();

      Console.WriteLine( $"Result: {thread.Result}" );
    }

    private static void StopCounting()
    {
      string readline = Console.ReadLine();
      _status = false;
    }

    private static void ExDateTimeFormat()
    {
      DateTime now = DateTime.Now;
      string path = "InstalledDate";
      File.Create( path ).Close();

      File.WriteAllText( path, DateTime.UtcNow.ToString( "O" ) );

      using( var reader = File.OpenText( path ) )
      {
        DateTimeOffset dateTimeOffset = DateTimeOffset.ParseExact( reader.ReadLine(), "O", CultureInfo.InvariantCulture );
        Console.WriteLine( dateTimeOffset.ToLocalTime().ToString( "MM/dd/yyyy hh:mm:ss tt" ) );
      }

      Console.WriteLine( now.ToString( "r", new CultureInfo( "en-US" ) ) );
      Console.WriteLine( now.ToString( "f", new CultureInfo( "en-US" ) ) );
      Console.WriteLine( now.ToString( new CultureInfo( "en-US" ) ) );
      Console.WriteLine( now.ToLocalTime().ToString( "f", new CultureInfo( "en-US" ) ) );
      Console.WriteLine( now.ToUniversalTime().ToString( "f", new CultureInfo( "en-US" ) ) );
    }

    private static void SetKey( JObject parent, JToken token, string newKey )
    {
      var tokenProp = token as JProperty;
      var oldKeyName = tokenProp.Name;
      parent[ newKey ] = tokenProp.Value;
      parent.Remove( oldKeyName );
    }

    private static void ExJsonEditWrite()
    {
      try
      {
        string fileTarget = Path.Combine( AppContext.BaseDirectory, "Configs", "VisionConfig.json" );

        if( !File.Exists( fileTarget ) )
        {
          Console.WriteLine( $"File {fileTarget} doesnt exist" );
          return;
        }

        var jObject = JObjectReader.FileToJObject( fileTarget );

        var data = jObject[ "VisionConfig" ];
        Console.WriteLine( JsonConvert.SerializeObject( data, Newtonsoft.Json.Formatting.Indented ) );

        JValue value = data[ "TresholdIrregulerTips" ] as JValue;
        value.Parent.Remove();
        data[ "ThresholdIrregulerTips" ] = value;
        data[ "SlantedTipsIndexResults" ] = JArray.FromObject( new List<int>()
        {
          1,
          2,
          4,
          5,
          6,
          7
        } );
        data[ "ThresholdSlantedTips" ] = 50;
        List<Dictionary<string, int>> listSlantedTipsThreshold = new List<Dictionary<string, int>>()
        {
          new Dictionary<string, int>()
          {
            [ "Index" ] = 0, [ "Threshold" ] = 50
          },
          new Dictionary<string, int>()
          {
            [ "Index" ] = 2, [ "Threshold" ] = 50
          },
          new Dictionary<string, int>()
          {
            [ "Index" ] = 4, [ "Threshold" ] = 50
          },
          new Dictionary<string, int>()
          {
            [ "Index" ] = 6, [ "Threshold" ] = 50
          },
          new Dictionary<string, int>()
          {
            [ "Index" ] = 8, [ "Threshold" ] = 50
          },
          new Dictionary<string, int>()
          {
            [ "Index" ] = 10, [ "Threshold" ] = 50
          }
        };
        data[ "SlantedTipsThreshold" ] = JArray.FromObject( listSlantedTipsThreshold );

        string content = JsonConvert.SerializeObject( data, Newtonsoft.Json.Formatting.Indented );
        File.WriteAllText( fileTarget, content );
      }
      catch( Exception e )
      {
        Console.WriteLine( e.Message );
      }
    }

    private static void ExConverCLRF()
    {
      string targetFile = "D:\\C#\\FastSoftwareUpdater\\SoftwareUpdater\\bin\\Debug\\Temp\\rc.local";

      File.WriteAllText( targetFile, File.ReadAllText( targetFile ).Replace( "\r\n", "\n" ) );
    }

    private static void ExSshClient()
    {
      //SshClient sshClient = new SshClient( "192.168.0.205", "pi", "raspberry" );
      PrivateKeyFile privateKeyFile = new PrivateKeyFile( "C:\\Users\\Formulatrix\\.ssh\\id_rsa" );
      SshClient sshClient = new SshClient( "192.168.0.201", "fast", privateKeyFile );
      sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds( 10 );
      sshClient.Connect();

      string appName = "Fast";
      //var command = sshClient.CreateCommand( $"ps aux | grep {appName}" );
      var command = sshClient.CreateCommand( $"/opt/formulatrix/utils/System/bin/rwro.sh ro" );
      var results = command.BeginExecute();
      string result = "";
      string errorCommand = "";
      string patternStr = $"./{appName}$";
      bool appExists = false;

      using( var reader = new StreamReader( command.OutputStream, Encoding.UTF8, true, 1024, true ) )
      {
        while (!results.IsCompleted || !reader.EndOfStream)
        {
          result = reader.ReadLine();
          if( !command.Error.Equals( "" ) )
          {
            errorCommand = command.Error;
            Console.WriteLine( errorCommand );
            break;
          }
          if( result != null )
          {
            Console.WriteLine( result );
            Regex pattern = new Regex( patternStr );
            if( pattern.IsMatch( result ) )
            {
              appExists = true;
              break;
            }
          }
          if( reader.EndOfStream )
          {
            break;
          }
        }
      }

      if( appExists )
        Console.WriteLine( $"{result.Split().Last()}, {appName} is running" );
      else
        Console.WriteLine( $"{appName} is not running" );
    }

    private static void ExModifyFileRemote()
    {
      //SftpClient sftpClient = new SftpClient( "192.168.0.147", "pcr", " " );
      SftpClient sftpClient = new SftpClient( "192.168.0.105", "pi", "raspberry" );
      sftpClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds( 10 );
      sftpClient.Connect();
      if( sftpClient.IsConnected )
        Console.WriteLine( "Connected" );
      else
        Console.WriteLine( "Failed to connect" );

      string filePath = "/etc/rc.local";

      using( FileStream fileStream = File.Open( Path.Combine( AppContext.BaseDirectory, "rc.local" ), FileMode.Create, FileAccess.ReadWrite ) )
      {
        sftpClient.DownloadFile( filePath, fileStream );
      }

      string[] allLines = File.ReadAllLines( Path.Combine( AppContext.BaseDirectory, "rc.local" ) );

      foreach( var item in allLines )
      {
        Console.WriteLine( item );
      }

      for( int ii = 0; ii < allLines.Length; ii++ )
      {
        if( Regex.IsMatch( allLines[ ii ], "cd /home/pi/Projects/FastVision-git/build/Formulatrix.Fast.Vision/" ) )
        {
          allLines[ ii ] = "cd /home/pi/projects/FastVision/build/Formulatrix.Fast.Vision/";
          break;
        }
      }

      foreach( var item in allLines )
      {
        Console.WriteLine( item );
      }

      File.WriteAllLines( Path.Combine( AppContext.BaseDirectory, "rc.local" ), allLines );

      using( FileStream fileStream = File.Open( Path.Combine( AppContext.BaseDirectory, "rc.local" ), FileMode.Open, FileAccess.Read ) )
      {
        SshClient sshClient = new SshClient( "192.168.0.147", "pcr", " " );
        sshClient.ConnectionInfo.Timeout = TimeSpan.FromSeconds( 10 );
        sshClient.Connect();

        var command = sshClient.CreateCommand( $"sudo chmod 766 {filePath}" );
        var results = command.BeginExecute();
        string result = "";
        string errorCommand = "";

        using( var reader = new StreamReader( command.OutputStream, Encoding.UTF8, true, 1024, true ) )
        {
          while (!results.IsCompleted || !reader.EndOfStream)
          {
            result = reader.ReadLine();
            if( !command.Error.Equals( "" ) )
            {
              errorCommand = command.Error;
              break;
            }
            if( result != null )
            {
              //do something;
            }
            if( reader.EndOfStream )
            {
              break;
            }
          }
        }

        Console.WriteLine( "Error command: " + errorCommand );

        sftpClient.UploadFile( fileStream, filePath, true );

        command = sshClient.CreateCommand( $"sudo chmod 755 {filePath}" );
        results = command.BeginExecute();
        result = "";
        errorCommand = "";

        using( var reader = new StreamReader( command.OutputStream, Encoding.UTF8, true, 1024, true ) )
        {
          while (!results.IsCompleted || !reader.EndOfStream)
          {
            result = reader.ReadLine();
            if( !command.Error.Equals( "" ) )
            {
              errorCommand = command.Error;
              break;
            }
            if( result != null )
            {
              //do something;
            }
            if( reader.EndOfStream )
            {
              break;
            }
          }
        }

        Console.WriteLine( "Error command: " + errorCommand );
        //  sshClient.Disconnect();
        //  sshClient.Dispose();
      }
    }

    private static void ExRegex()
    {
      //string test = "#define VERSION \"v0.1.0-48-g6f57799\"";
      string test = "umount: /opt/formulatrix/FastInstrumentTemp/TrayDoc.fast: not mounted";
      //var match = Regex.Match( test, @".+v(\d+\.\d+\.\d+).+" );
      //var match = Regex.Match( test, @"not mounted" );
      var match = Regex.Match( test, @"(Fast\w+)" );
      if( match.Success )
      {
        Console.WriteLine( "Found" );
        Console.WriteLine( match.Groups.First().Value );
        //foreach( Group item in match.Groups )
        //{
        //  Console.WriteLine( item.Value );
        //}
      }
      else
        Console.WriteLine( "Not found" );
    }

    private static void ExSortListVersion()
    {
      var listVersion = new List<Version>()
      {
        new Version( "1.1.1.2" ), new Version( "1.1.1.1" ), new Version( "1.1.1.3" )
      };
      listVersion.Sort();
      listVersion.Reverse();

      foreach( var version in listVersion )
        Console.WriteLine( version.ToString() );

      Version version0 = new Version( 1, 2, 3, 4 );
      Version version1 = new Version( 1, 2, 3, 4 );
      Version version2 = new Version( 1, 2, 3, 5 );

      if( version0 == version1 ) Console.WriteLine( $"{nameof( version0 )} == {nameof( version1 )}" );
      else Console.WriteLine( $"{nameof( version0 )} != {nameof( version1 )}" );
      if( version0 == version2 ) Console.WriteLine( $"{nameof( version0 )} == {nameof( version2 )}" );
      else Console.WriteLine( $"{nameof( version0 )} != {nameof( version2 )}" );
    }

    private static void ExXmlFile()
    {
      try
      {
        // Update selector.xml
        string srcPath = Path.Combine( AppContext.BaseDirectory, "Configs", "selector.xml" );

        if( File.Exists( srcPath ) )
          Console.WriteLine( $"File {srcPath} exists" );

        XElement selectorXml = XElement.Load( srcPath );

        //// PlungersPosition
        //XElement plungerPosition = selectorXml.Element( "PlungersPosition" );
        //List<XElement> points = plungerPosition.Descendants( "Point" ).Select( ( p ) => p ).ToList();
        //points[ 0 ].Attribute( "x" ).SetValue( 233 );
        //points[ 0 ].Attribute( "y" ).SetValue( 155 );
        //points[ 1 ].Attribute( "x" ).SetValue( 449 );
        //points[ 1 ].Attribute( "y" ).SetValue( 155 );
        //points[ 2 ].Attribute( "x" ).SetValue( 449 );
        //points[ 2 ].Attribute( "y" ).SetValue( 290 );
        //points[ 3 ].Attribute( "x" ).SetValue( 233 );
        //points[ 3 ].Attribute( "y" ).SetValue( 290 );

        //// SlantedTipsParam
        XElement slantedTipsParam = new XElement( "SlantedTipsParam" );
        XAttribute xAttr = new XAttribute( "x", 6 );
        XAttribute yAttr = new XAttribute( "y", 4 );
        XAttribute widthAttr = new XAttribute( "width", 48 );
        XAttribute heightAttr = new XAttribute( "height", 48 );
        slantedTipsParam.Add( xAttr, yAttr, widthAttr, heightAttr );
        selectorXml.Add( slantedTipsParam );

        //// SlantedTipsModel
        if( selectorXml.Element( "SlantedTipsModel" ) == null )
          Console.WriteLine( "No SlantedTipsModel" );
        else
          Console.WriteLine( "SlantedTipsModel exists" );

        XElement slantedTipsModel = new XElement( "SlantedTipsModel" );
        XElement group = new XElement( "Group" );
        XAttribute attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q1_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        group = new XElement( "group" );
        attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q2_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        group = new XElement( "group" );
        attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q3_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        group = new XElement( "group" );
        attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q4_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        group = new XElement( "group" );
        attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q5_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        group = new XElement( "group" );
        attr = new XAttribute( "path", "../../Formulatrix.Fast.Vision/models/slanted_tip_q6_100_96_v2_edgetpu.tflite" );
        group.Add( attr );
        slantedTipsModel.Add( group );

        selectorXml.Add( slantedTipsModel );

        selectorXml.Save( srcPath );

        srcPath = Path.Combine( AppContext.BaseDirectory, "Configs", "config.xml" );
        selectorXml = XElement.Load( srcPath );
        selectorXml.Save( srcPath );
      }
      catch( Exception e )
      {
        Console.WriteLine( e.Message );
      }
    }

    private static void ExCreateLongDirectory()
    {
      try
      {
        var dir1 = Directory.CreateDirectory( "X:\\a\\b\\c\\f\\d\\e\\lalal.txt" );

        if( dir1.Exists )
          Console.WriteLine( $"{dir1.FullName} exists" );
        else
          Console.WriteLine( $"failed create {dir1.FullName}" );

        var file1 = new FileInfo( "X:\\a\\b\\dasd.txt" );
        Console.WriteLine( file1.Directory.FullName );

        //var dir2 = Directory.CreateDirectory( "X:\\a\\b\\c\\d\\e" );

        //if( dir2.Exists )
        //  Console.WriteLine( $"{dir1.FullName} exists" );
        //else
        //  Console.WriteLine( $"failed create {dir1.FullName}" );
      }
      catch( Exception e )
      {
        Console.WriteLine( e.Message );
      }
    }

    private static void ExReadVersionAssembly()
    {
      var currentEnvDirectory = Environment.CurrentDirectory;
      var configFolderPath = Path.Combine( Directory.GetParent( currentEnvDirectory ).Parent.ToString(), "Release\\netcoreapp2.1\\linux-arm\\publish\\FirstApp.dll" );
      Console.WriteLine( configFolderPath );

      var version = AssemblyName.GetAssemblyName( configFolderPath ).Version;
      Console.WriteLine( $"Assembly version: {version}" );
    }

    private static void ExBitmask()
    {
      int number = 16; // 00010000
      number |= 1 << 3; //    00010000
      // OR 00001000 <= 00000001 shift left 3 bits
      //    00011000
      Console.WriteLine( number );
    }

    private static void ExDeserializeNestingClass()
    {
      var newParent = new Parent()
      {
        ChildAge = new Child2()
        {
          Age = 20
        },
        ChildName = new Child1()
      };
      Console.WriteLine( $"Deserialize Result : {JsonConvert.SerializeObject( newParent )}" );
    }

    private static void ExSwitchCaseNobreak()
    {
      int a = 0;
      switch( a )
      {
        case 0:
        case 2:
          Console.WriteLine( "Enter 2" );
          break;
        default:
          break;
      }
    }

    private static void ExMultipleAssignment()
    {
      int a = 2, b = 6;
      Console.WriteLine( $"{a} {b}" );
      a = b = 50;
      Console.WriteLine( $"{a} {b}" );
    }

    private static void ExTimer()
    {
      System.Timers.Timer timer = new System.Timers.Timer
      {
        Interval = 1000.0
      };
      timer.Elapsed += new ElapsedEventHandler( ElapsedEvent );
      timer.Enabled = true;
      Console.ReadLine();
    }

    private static void ElapsedEvent( object source, ElapsedEventArgs e )
    {
      Console.WriteLine( e.SignalTime.ToLocalTime().ToString() );
    }

    private static void ExReverseSearchList()
    {
      var listTemp = new List<int>()
      {
        1,
        4,
        5,
        2,
        5,
        1,
        2,
        6,
        1,
        4
      };
      var trueIdx = listTemp.FindLastIndex( ( val ) => val == 1 );
      var falseIdx = listTemp.FindLastIndex( ( val ) => val == 0 );
      Console.WriteLine( $"true {trueIdx}, false {falseIdx}" );
      var listAdvTemp = new List<List<int>>()
      {
        new List<int>()
        {
          1, 5, 6
        },
        new List<int>()
        {
          0, 5, 6
        },
        new List<int>()
        {
          1, 0, 6
        },
        new List<int>()
        {
          1, 2, 4
        }
      };
      var trueIdxAdv = listAdvTemp.FindLastIndex( ( listInt ) => listInt.Contains( 0 ) );
      var falseIdxAdv = listAdvTemp.FindLastIndex( ( listInt ) => listInt.Contains( 8 ) );
      Console.WriteLine( $"true {trueIdxAdv}, false {falseIdxAdv}" );
    }

    private static void ExCheckOutParam()
    {
      int[] data = new int[]
      {
        1, 2, 4, 5, 6
      };
      var outTest = new OutToClass( data );
      foreach( var val in data )
      {
        Console.WriteLine( val );
      }

      outTest.StartChangeData();

      foreach( var val in data )
      {
        Console.WriteLine( val );
      }
    }

    private static void ExTestEnumFunc()
    {
      Console.WriteLine( "If int is not in enum, It will have type as enum but without any mapping\n" );

      TestEnum testSuccess = (TestEnum)1;
      TestEnum testFail = (TestEnum)3;
      int intSuccess = (int)testSuccess;
      int intFail = (int)testFail;

      Console.WriteLine( $"SUCCESS enum: {testSuccess}, int: {intSuccess}" );
      Console.WriteLine( $"FAILED  enum: {testFail}, int: {intFail}" );

      string error = "Error";
      Enum.TryParse( error, out TestEnum result );
      Console.WriteLine( $"SUCCESS enum: {result}, int: {(int)result}" );
      Enum.TryParse( "sadsa", out result );
      Console.WriteLine( $"FAILED  enum: {result}, int: {(int)result}" );
    }

    enum TestEnum
    {
      Info = 0,
      Warning = 1,
      Error = 2
    };

    private static void ExGetClassFromString()
    {
      string className = "EventClass";
      Type type = Type.GetType( className );
      object eventClass = Activator.CreateInstance( type );
      //Console.WriteLine( eventClass?.GetType() ?? "null value" );
    }

    private static void ExFileDirectoryPath()
    {
      string[] listdata =
      {
        "lalal", "dididi", "tototo"
      };
      Console.WriteLine( "test list data {0}", JsonConvert.SerializeObject( listdata ) );

      var currentEnvDirectory = Environment.CurrentDirectory;
      var currentDirDirectory = Directory.GetCurrentDirectory();
      var currentAsmEntryDirectory = Path.GetDirectoryName( Assembly.GetEntryAssembly().Location );
      var currentAsmExecDirectory = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );

      Console.WriteLine( currentEnvDirectory );
      Console.WriteLine( currentDirDirectory );
      Console.WriteLine( currentAsmEntryDirectory );
      Console.WriteLine( currentAsmExecDirectory );

      var configFolderPath = Path.Combine( Directory.GetParent( currentEnvDirectory ).Parent.Parent.ToString(), "Configs" );
      Console.WriteLine( configFolderPath );
      var listFiles = Directory.GetFiles( configFolderPath );
      foreach( var file in listFiles )
      {
        Console.WriteLine( file );
        using( var streamReader = new StreamReader( file ) )
        {
          var content = streamReader.ReadToEnd();
          Console.WriteLine( content );
          Dictionary<string, dynamic> contentData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>( content );
          foreach( var entry in contentData )
          {
            Console.WriteLine( entry.Key + " " + entry.Value );
          }

          streamReader.Close();
          contentData[ "code" ] = 404;
          string json = JsonConvert.SerializeObject( contentData );
          File.WriteAllText( file, json );
        }
      }
    }

    private static void ExNameOf()
    {
      //throw new NotImplementedException();
      Console.WriteLine( $"{nameof( ExNameOf )} is called" );
    }

    private static void ExMethodExtensionClass()
    {
      Console.WriteLine( "===EXAMPLE CLASS EXTENSION===" );
      Console.WriteLine( new EventClass().AddHello() );
      Console.WriteLine();
    }

    private static async void ExLINQ()
    {
      Console.WriteLine( "===EXAMPLE LINQ===" );
      try
      {
        client.DefaultRequestHeaders.Add( "app-id", "5fe1440be2a1ce246c33047d" );

        var jsonString = await client.GetStringAsync( "https://dummyapi.io/data/api/user?limit=10" );

        Console.WriteLine( "Success" );

        QuickType.UserModelWrapper responseObj = JsonConvert.DeserializeObject<QuickType.UserModelWrapper>( jsonString );
        Console.WriteLine( responseObj.Limit );

        foreach( var user in responseObj.Data )
          user.PrintData();

        IEnumerable<string> listUser = responseObj.Data
          .Where( user => user.Title == "miss" )
          .OrderBy( user => user.FirstName ).ThenBy( user => user.LastName )
          .Select( user => user.FirstName + " " + user.LastName );

        Console.WriteLine( "\n\nFILTERED DATA" );
        foreach( var user in listUser )
          Console.WriteLine( user );
        ;
      }
      catch( Exception e )
      {
        Console.WriteLine( "Error: {0}", e.Message );
      }
      Console.WriteLine();
    }

    private static void ExEventDelegate()
    {
      Console.WriteLine( "===EXAMPLE EVENT DELEGATE===" );
      EventClass eventClass = new EventClass()
      {
        ClassName = "Beer Party"
      };

      eventClass.SpacePressed -= Subscriber1;
      eventClass.SpacePressed -= Subscriber2;
      eventClass.SpacePressed -= Subscriber1;
      eventClass.SpacePressed += Subscriber1;
      eventClass.TestEventHandler += BoolSubscriber;
      eventClass.KeyboardInputHandler( "New message incoming" );
      Console.WriteLine();
    }

    private static void BoolSubscriber( object source, bool info )
    {
      Console.WriteLine( "Bool subsriber: bool info: {0}", info );
    }

    private static void Subscriber1( object source, CustomEventArgs e )
    {
      EventClass obj = (EventClass)source;
      Console.WriteLine( "First subscriber: event args {0}", e.Title );
    }

    private static void Subscriber2( object s, CustomEventArgs e )
    {
      var eventClass = s as EventClass;
      Console.WriteLine( "Second subscriber: class name {0}", eventClass.ClassName );
    }

    private delegate Task DelegateTask();

    private delegate void DelegateVoid();

    private static readonly HttpClient client = new HttpClient();

    private static async Task ExHttpRequest()
    {
      client.DefaultRequestHeaders.Add( "app-id", "5fe1440be2a1ce246c33047d" );

      var response = await client.GetAsync( "https://dummyapi.io/data/api/user?limit=10" );

      if( response.IsSuccessStatusCode )
      {
        Console.WriteLine( "Success" );
        var jsonString = await response.Content.ReadAsStringAsync();

        QuickType.UserModelWrapper responseObj = JsonConvert.DeserializeObject<QuickType.UserModelWrapper>( jsonString );
        Console.WriteLine( responseObj.Limit );

        foreach( var user in responseObj.Data )
          user.PrintData();
      }
      else
        Console.WriteLine( "Error : {0}", (int)response.StatusCode );
    }

    private static async Task ExAsyncAwaitProgress()
    {
      long jj = 0;

      void progres( long i )
      {
        if( i != jj )
        {
          Console.WriteLine( i + "%" );
          jj = i;
        }
      }

      ;

      await Foo( progres );
    }

    private static Task Foo( Action<long> progressChanged )
    {
      return Task.Run( () =>
      {
        for( long i = 0; i <= 10000000000; i++ )
        {
          if( i % 100000000 == 0 )
            progressChanged( i / 100000000 );
        }
      } );
    }

    private static async Task<string> ExAsyncAwait()
    {
      //Task<int> task = Task.Run( async () => { await Task.Delay( 2000 ); Console.WriteLine( "Async done!!" ); return 1; } );
      Task<string> msg = GetMessageAsync( "Test get msg" );

      Task<A> testTask = Task.Run( async () =>
      {
        await Task.Delay( 3000 );
        return new A();
      } );

      Console.WriteLine( "Main flow!" );

      var timeOutFunc = Task.Run( async () => await Task.Delay( 5000 ) );

      if( await Task.WhenAny( msg, testTask, timeOutFunc ) != timeOutFunc )
      {
        string msgRes = await msg;

        Console.WriteLine( "GetMessage done" );
        Console.WriteLine( msgRes );

        A a = await testTask;
        a.Test();

        return await msg;
      }
      else
      {
        Console.WriteLine( "Time out" );
        return null;
      }

      //Set timeout
      //if( !task.Wait( TimeSpan.FromMilliseconds( 1000 ) ) )
      //  Console.WriteLine( "Time out" );
    }

    private static async Task<string> GetMessageAsync( string msg )
    {
      await Task.Delay( 2000 );


      return msg;
    }

    private static void ExThreading()
    {
      Thread thread = new Thread( WriteY );
      thread.Start();
      //WriteY();

      for( int i = 0; i < 1e6; i++ )
      {
        Console.Write( "O" );
      }

      thread.Join();
    }

    private static void WriteY()
    {
      for( int i = 0; i < 1e6; i++ )
      {
        Console.Write( "X" );
      }
    }

    private static void ExInterfaceAbstract()
    {
      var impClass = new ImplementationAbstract
      {
        Counter = 200
      };
      impClass.ShowFoo();
    }

    static private void ExVirtOverHide()
    {
      var a = new A();
      var b = new B();
      var c = new C();

      //Overriding and Hiding have same result
      a.Test();
      b.Test();
      c.Test();

      Console.WriteLine();
      //Overriding and Hiding have different result
      a = c as A;
      b = c as B; // Hiding means that subclass component is hidden, base class implemented instead
      a.Test();
      b.Test(); // Hiding : B::Test()     Overriding: C::Test()
      c.Test(); //          C::Test()                 C::Test()
    }

    static private void ExInheritanceCasting()
    {
      // Upcast
      var newChildClass = new ChildClass( 20, "tony" );
      GrandChildClass newGrandChildClass = new GrandChildClass( 52, "Lala", "Berkeley" );
      var newParentClass = newChildClass as ParentClass;
      newParentClass.DisplayInParent();

      ChildClass newChildClass2 = newParentClass as ChildClass;
      newChildClass2.DisplayInChild();

      newGrandChildClass.DisplayInGrandChild();
      newGrandChildClass.DisplayInChild();
      newGrandChildClass.DisplayInParent();

      // Downcast
      var newParentClass2 = new ParentClass( "papa" );
      ChildClass newChild = newParentClass2 as ChildClass;
      //newChild.DisplayInParent(); // Failed due to not parent obj
      //newChild.DisplayInChild(); // Failed due to data lost

      // check identity
      newChildClass.CheckWhichChild();
      SecondChildClass secondChildClass = new SecondChildClass();
      secondChildClass.CheckWhichChild();
    }

    private static void ExLib() => Mathe.Multiply();

    private static void ExLib( string msg ) => Console.WriteLine( msg );

    private static void ExLoop()
    {
      foreach( int val in new int[]
      {
        1, 2, 3, 4
      } )
      {
        Console.WriteLine( val );
      }

      int varGoto = 0;
      labelGoto:
      Console.WriteLine( varGoto++ );
      if( varGoto < 6 )
        goto labelGoto;

      varGoto = 0;
      for( ;; )
        if( varGoto < 3 )
          Console.WriteLine( ++varGoto );
        else
          break;
    }

    static private void ExProperties()
    {
      var newCar = new Car<int>( 52 );
      Console.WriteLine( newCar.HiddenProp );
      newCar.HiddenProp = 42;
      Console.WriteLine( newCar.HiddenProp );
    }

    static private void PrintArguments( int x, int y = 100, params int[] args )
    {
      Console.WriteLine( $"{x} and {y}" );
      foreach( var data in args )
        Console.WriteLine( data );
    }

    static private void ExPassByRefNoKeyword( dynamic refin )
    {
      refin.Name = "Herry";
    }

    static private void ExPassByref( ref int refin )
    {
      refin = 42;
    }

    static private void ExArray()
    {
      int sizeOfArr = Convert.ToInt32( Console.ReadLine() );
      int[] intArr = new int[ sizeOfArr ];

      for( int i = 0; i < intArr.Length; i++ )
      {
        intArr[ 0 ] = 1;
        intArr[ 1 ] = 24;
        intArr[ 2 ] = 56;
      }
      foreach( var data in intArr )
        Console.WriteLine( data );

      List<int> intList = new List<int>();
      intList.InsertRange( 0, intArr );

      foreach( var intData in intList )
        Console.WriteLine( intData );
    }

    static private void ExString()
    {
      int age = 45;
      Console.WriteLine( $"Interpolated {age}" );
      Console.WriteLine( "String Format {0}", age );
    }

    static private void NullableExample()
    {
      int? nullInt = null;
      //int nullInt2 = null; Illegal
      Console.WriteLine( nullInt?.ToString() ?? "data is null" );
      Car<int> a = new Car<int>( 7 ), b = a;
      Console.WriteLine( a.Number );
      Console.WriteLine( b.Number );
      b = null;
      Console.WriteLine( b?.Number.ToString() ?? "car is null" );
    }

    static private void ExDoubleParsing()
    {
      string[] values =
      {
        "1,643.57", "$1,643.57", "-1.643e6", "-168934617882109132", "123AE6", null, string.Empty, "ABCDEF"
      };

      NumberFormatInfo provider = new NumberFormatInfo
      {
        NumberDecimalSeparator = ".", NumberGroupSeparator = ","
      };

      foreach( var value in values )
      {
        if( double.TryParse( value, NumberStyles.Any, provider, out double number ) )
          Console.WriteLine( "'{0}' --> {1}", value, number );
        else
          Console.WriteLine( "Unable to parse '{0}' --> {1}", value, number );
      }
    }

    private string GetName( string name )
    {
      return Number( "4125" ).ToString();
      //return string.Format("My name is {0}, with number {1}", name == null ? "default" : name, number());
    }

    private int Number()
    {
      return 5;
    }

    private int Number( string strConverted )
    {
      return Convert.ToInt32( strConverted );
    }

    static private void ExCopyByValue()
    {
      int a = 7, b = a;
      Console.WriteLine( "a {0}", a );
      Console.WriteLine( "b {0}", b );
      a = 9;
      Console.WriteLine( "a {0}", a );
      Console.WriteLine( "b {0}", b );
    }

    static private void ExCopyByReference()
    {
      Car<int> a = new Car<int>( 12 ), b = a;
      Console.WriteLine( "a {0}", a.Number );
      Console.WriteLine( "b {0}", b.Number );
      a.Number = 9;
      Console.WriteLine( "a {0}", a.Number );
      Console.WriteLine( "b {0}", b.Number );
    }
  }

  class Car<T>
  {
    private int hiddenProp = 12;

    public int HiddenProp { set => hiddenProp = 15; get => hiddenProp; }
    public int Number { set; get; }

    public Car( int number ) => Number = number;
    public Car( int number, int hidden ) : this( number ) => HiddenProp = hidden;
  }

  partial class PartialClass
  {
    private void CallingClass() => Console.WriteLine( "Calling partial class" );
  }
}
