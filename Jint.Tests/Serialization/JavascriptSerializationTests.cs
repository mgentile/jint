using Jint.Parser.Ast;
using Jint.Serialization;
using System;
using System.Net;
using Xunit;

namespace Jint.Tests.Serialization
{
    public class JavascriptSerializationTests : IDisposable
    {
        private readonly Engine _engine;

        public JavascriptSerializationTests()
        {
            _engine = new Engine()
                .SetValue("log", new Action<object>(Console.WriteLine))
                .SetValue("assert", new Action<bool>(Assert.True));
        }

        void IDisposable.Dispose()
        {
        }


        private void RunTest(string source)
        {
            var parser = new Jint.Parser.JavaScriptParser();
            Program program = parser.Parse(source);
            var serializationResult = JsonFormatter.Serialize(program);

            _engine.Execute(JsonFormatter.Deserialize<Program>(serializationResult));

            _engine.Execute(source);
        }

        [Fact]
        public void ShouldSerializeScripts()
        {
            var scriptCode = @"
                var Vehicle = function () {};
                var vehicle = new Vehicle();
                assert(vehicle != undefined);
            ";

            RunTest(scriptCode);
        }

        [Theory]
        [InlineData("access-binary-trees", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/access-binary-trees.js")]
        [InlineData("bitops-3bit-bits-in-byte", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/bitops-3bit-bits-in-byte.js")]
        [InlineData("crypto-md5", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/crypto-md5.js")]
        [InlineData("crypto-sha1", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/crypto-sha1.js")]
        [InlineData("date-format-tofte", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/date-format-tofte.js")]
        [InlineData("date-format-xparb", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/date-format-xparb.js")]
        [InlineData("math-partial-sums", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/math-partial-sums.js")]
        [InlineData("regexp-dna", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/regexp-dna.js")]
        [InlineData("string-base64", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/string-base64.js")]
        [InlineData("string-fasta", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/string-fasta.js")]
        [InlineData("string-tagcloud", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/string-tagcloud.js")]
        [InlineData("string-unpack-code", "https://raw.githubusercontent.com/WebKit/webkit/master/PerformanceTests/SunSpider/tests/sunspider-1.0.2/string-unpack-code.js")]
        public void ShouldExecuteSunSpiderTestsAfterDeserialization(string name, string url)
        {
            var content = new WebClient().DownloadString(url);
            RunTest(content);
        }
    }
}
