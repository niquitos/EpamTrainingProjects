var createState = function () {
    return "SessionValuelaksejroiaueroiaserfzxdvnzxfkaiueroaifdkljaksjdhfkjhasdkfj";
};
var createNonce = function () {
    return "NonceValueqoweiweopritioshdkfhskdchvwsiotusadoifvzxhcvkjhsaoeriuoeroiqlzkdhfv";
};

var signIn = function () {
    var redirectUri = "https://localhost:44308/Home/SignIn";
    var responseType = "id_token token";
    var scope = "openid ApiOne";
    var authUrl = "/connect/authorize/callback" +
        "?client_id=client_id_js" +
        "&redirect_uri=" + redirectUri +
        "&response_type=" + responseType +
        "&scope=" + scope +
        "&nonce=" + createNonce() +
        "&state=" + createState();
    var returnUrlValue = encodeURIComponent(authUrl);

    window.location.href = "https://localhost:44360/Auth/Login?ReturnUrl=" + returnUrlValue;
}