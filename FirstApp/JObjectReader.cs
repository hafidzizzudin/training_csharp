using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FirstApp
{
  class JObjectReader
  {
    public static JObject FileToJObject( string path )
    {
      using( StreamReader file = File.OpenText( path ) )
      {
        using( JsonTextReader reader = new JsonTextReader( file ) )
        {
          return (JObject) JToken.ReadFrom( reader );
        }
      }
    }
  }
}
