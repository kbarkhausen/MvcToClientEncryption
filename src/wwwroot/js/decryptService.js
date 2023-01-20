var decryptService = {
   
    getKeys: function () {
        var result = "";
        $.ajax({
            url: "encryption/keys",
            type: 'GET',
            async: false,
            cache: false,
            timeout: 30000,           
            success: function (data) {
                result = data;
            }
        });
        return result;
    },
    decrypt: function (input, config) {
        try {

            var config = this.getKeys();

            console.log("input = " + input);
            console.log("config.iv = " + config.iv);
            console.log("config.password = " + config.password);
            console.log("config.salt = " + config.salt);

            //Creating the Vector Key
            var iv = CryptoJS.enc.Hex.parse(config.iv);
            //Encoding the Password in from UTF8 to byte array
            var Pass = CryptoJS.enc.Utf8.parse(config.password);
            //Encoding the Salt in from UTF8 to byte array
            var Salt = CryptoJS.enc.Utf8.parse(config.salt);
            //Creating the key in PBKDF2 format to be used during the decryption
            var key128Bits1000Iterations = CryptoJS.PBKDF2(Pass.toString(CryptoJS.enc.Utf8), Salt, { keySize: 128 / 32, iterations: 1000 });
            //Enclosing the test to be decrypted in a CipherParams object as supported by the CryptoJS libarary
            var cipherParams = CryptoJS.lib.CipherParams.create({
                ciphertext: CryptoJS.enc.Base64.parse(input)
            });

            //Decrypting the string contained in cipherParams using the PBKDF2 key
            var decrypted = CryptoJS.AES.decrypt(cipherParams, key128Bits1000Iterations, { mode: CryptoJS.mode.CBC, iv: iv, padding: CryptoJS.pad.Pkcs7 });

            var result = decrypted.toString(CryptoJS.enc.Utf8);

            if (result.length === 0)
                throw new Error('Input value is invalid and cannot be decrypted.');

            console.log("decrypted (by JS) = " + result);
            return result;
        }
        catch (err) {
            return err;
        }
    }
}