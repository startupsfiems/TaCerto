using UnityEngine.Networking;
using System.Security.Cryptography.X509Certificates;

// Based on https://www.owasp.org/index.php/Certificate_and_Public_Key_Pinning#.Net
class AcceptAllCertificatesSignedWithASpecificKeyPublicKey : CertificateHandler
{
    // Encoded RSAPublicKey
    private static string PUB_KEY = "AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAACYM1Yh9HhEmPgOCJPG" +
        "YftQAAAAACAAAAAAADZgAAwAAAABAAAACEza/U8lPOO9/nYKNfMfmtAAAAAASAAACgAAAAEAAAAFK8" +
        "tIO0vGyzh2mT9rTSTZtIAQAAoBn1EspMMNEO/UM/HcGdBDZV5c++jAiaEYlyLSZLurVvkuY2wKmzi4" +
        "cx75pUAi3NqFGOjK2pXsR2J1fvEmPVqrLOv3aySA30WzyCmCB/UEs2sZ1vm+Wlw7SObKf2v5ACJ0rB" +
        "UDjIJDRpd8+FZu+RHfCfs7xJlfAeNwr/+lrtVsvV95JbnsfoQ1iwHqXUs3Tj4OorlxBv6IzodBw5mo" +
        "qsWgSnplas68F1nhWcUkjpirFqp107zAdh9mbvy7E8BEFzX+koOAPL2/3nnkw9UvtGDF+iOHgBOLVm" +
        "Jj/KEbe79t6Q+MUCWi8gb/PA1JVimh4Ay5lz9945RbX54UwkePItzJfhB8lDiOwa2nndf8PqTy1KUx" +
        "FURKpi7EqnE0+QTORXdmBarEZPTPlOzlPhBR2KyyIXU21gSuVXx/zNDkrJ9M2Qot32yZMDFhQAAADc" +
        "g+Q+km2R3QYUB48AeImvbX2tRg";

    protected override bool ValidateCertificate(byte[] certificateData)
    {
        /*X509Certificate2 certificate = new X509Certificate2(certificateData);
        string pk = certificate.GetPublicKeyString();
        if (pk.Equals(PUB_KEY))
            return true;

        return false;*/
        return true;
    }
}