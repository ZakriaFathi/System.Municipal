using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Municipal.Identity.Api.Extensions;

public class RsaKeyService
    {
        private string File => Path.Combine(_environment.ContentRootPath, "rsakey.json");

        private readonly IWebHostEnvironment _environment;
        private readonly TimeSpan _timeSpan;

        public RsaKeyService(IWebHostEnvironment environment, TimeSpan timeSpan)
        {
            _environment = environment;
            _timeSpan = timeSpan;
        }

        public bool NeedsUpdate()
        {
            if (!System.IO.File.Exists(File)) return true;
            var creationDate = System.IO.File.GetCreationTime(File);
            return DateTime.Now.Subtract(creationDate) > _timeSpan;
        }

        public RSAParameters GetRandomKey()
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            try
            {
                return rsa.ExportParameters(true);
            }
            finally
            {
                rsa.PersistKeyInCsp = false;
            }
        }

        public RsaKeyService GenerateKeyAndSave(bool forceUpdate = false)
        {
            if (!forceUpdate && !NeedsUpdate()) return this;
            var p = GetRandomKey();
            var t = new RSAParametersWithPrivate();
            t.SetParameters(p);
            System.IO.File.WriteAllText(File, JsonConvert.SerializeObject(t, Formatting.Indented));
            return this;
        }

        public RSAParameters GetKeyParameters()
        {
            if (!System.IO.File.Exists(File)) throw new FileNotFoundException("Check configuration - cannot find auth key file: " + File);
            var keyParams = JsonConvert.DeserializeObject<RSAParametersWithPrivate>(System.IO.File.ReadAllText(File));
            return keyParams.ToRSAParameters();
        }

        public RsaSecurityKey GetKey()
        {
            if (NeedsUpdate()) GenerateKeyAndSave();
            var provider = new RSACryptoServiceProvider();
            provider.ImportParameters(GetKeyParameters());
            return new RsaSecurityKey(provider);
        }

        private class RSAParametersWithPrivate
        {
            public byte[] D { get; set; }
            public byte[] DP { get; set; }
            public byte[] DQ { get; set; }
            public byte[] Exponent { get; set; }
            public byte[] InverseQ { get; set; }
            public byte[] Modulus { get; set; }
            public byte[] P { get; set; }
            public byte[] Q { get; set; }

            public void SetParameters(RSAParameters p)
            {
                D = p.D;
                DP = p.DP;
                DQ = p.DQ;
                Exponent = p.Exponent;
                InverseQ = p.InverseQ;
                Modulus = p.Modulus;
                P = p.P;
                Q = p.Q;
            }
            public RSAParameters ToRSAParameters()
            {
                return new RSAParameters()
                {
                    D = D,
                    DP = DP,
                    DQ = DQ,
                    Exponent = Exponent,
                    InverseQ = InverseQ,
                    Modulus = Modulus,
                    P = P,
                    Q = Q

                };
            }
        }
    }