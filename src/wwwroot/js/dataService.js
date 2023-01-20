var dataService = {   
    get: function () {
        var result = "";

        $.ajax({
            url: "data",
            type: 'GET',
            async: false,
            cache: false,
            timeout: 30000,
            success: function (data) {
                result = data;
            }
        });

        var data = decryptService.decrypt(result);

        return JSON.parse(data);
    }
}