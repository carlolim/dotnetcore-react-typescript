export const addNovel = (data) => {
    data = {
        title: 'title',
        author: 'author',
        description: 'description'
    };
    console.log(data);
    return new Promise((resolve, reject) => {
        fetch("api/novel", {
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
            resolve(data);
        });
    });
}