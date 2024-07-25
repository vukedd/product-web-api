let SignInButton = document.getElementById("LogInBtn")
let LogInForm = document.getElementById("LogInForm")

SignInButton.addEventListener("click", function LogInRequest() {
    var PasswordData = document.getElementById("passwordData").value;
    var UsernameData = document.getElementById("usernameData").value;
    var lgnBtn = document.getElementById("lgnBtn")
    async function postData(data) {
        const url = 'https://localhost:7293/api/Account/login';

        try {
            const response = await axios.post(url, data, {
                headers: {
                    'Content-Type': 'application/json',
                }
            });
            
            sessionStorage.setItem("JWT", response.data.token)
            location.reload()
            window.location.href = "indexloggedin.html";
        } catch (error) {
            console.error('There has been a problem with your axios operation:', error);
        }
    }

    const myData = {
            username: UsernameData, 
            password: PasswordData
    };
    postData(myData);
});