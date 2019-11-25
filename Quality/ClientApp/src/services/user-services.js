export const signIn = (data) => {
    return new Promise((resolve, reject) => {
        fetch("api/user/login", {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(response => {
            if (response.status === 200) {
                return response.json();
            }
            return { isSuccess: false, message: "An error occured" };
        }).then(data => {
            if (data.isSuccess) {
                localStorage.clear();
                localStorage.setItem('quality_token', data.message);
                window.location.href = "/";
            }
            resolve(data);
        });
    });
}