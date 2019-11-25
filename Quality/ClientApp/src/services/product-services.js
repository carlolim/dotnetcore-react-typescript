export const productsList = (filter) => {
    return new Promise((resolve, reject) => {
        fetch("api/product/index", {
            method: 'POST',
            credentials: 'same-origin',
            headers: {
                'Accept': 'application/json, text/plain, */*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(filter)
        }).then(response => {
            if (response.status === 200) {
                return response.json();
            }
            return { isSuccess: false, message: "An error occured" };
        }).then(data => {
            resolve(data);
        });
    });
}