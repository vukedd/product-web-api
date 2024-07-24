import axios from 'axios'

var requestUrl = "https://localhost:7293/api/account/login";

let SignInButton = document.getElementById("LogInBtn")

SignInButton.addEventListener("click", LogInRequest)
function LogInRequest()
{
    let request = new XMLHttpRequest();
    request.onreadystatechange() = function() {
    if (this.readyState == 4)
        {
            console.log("a")
        }
    }
    request.open("POST", requestUrl);
    request.send();
}