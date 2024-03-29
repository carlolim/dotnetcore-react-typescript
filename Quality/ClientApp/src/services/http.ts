export interface IHttpResponse<T> extends Response {
    parsedBody?: T;
}

export const http = <T>(request: RequestInfo): Promise<IHttpResponse<T>> => {
    return new Promise((resolve, reject) => {
        let response: IHttpResponse<T>;
        fetch(request)
            .then(res => {
                response = res;
                return res.json();
            })
            .then(body => {
                if (response.ok) {
                    response.parsedBody = body;
                    resolve(response);
                }
                // else if (response.status === 401) {
                //     window.location.href = "/login";
                // }
                // else if (response.status === 403) {
                //     response.parsedBody = body;
                //     reject(response);
                // }
                else {
                    reject(response);
                }
            })
            .catch(err => {
                reject(err);
            });
    });
};

export const get = async <T>(
    path: string,
    args: RequestInit = { method: "get" }
): Promise<IHttpResponse<T>> => {
    return await http<T>(new Request(path, args));
};

export const post = async <T>(
    path: string,
    body: any,
    args: RequestInit = {
        method: "post",
        body: JSON.stringify(body),
        credentials: 'same-origin',
        headers: {
            'Accept': 'application/json, text/plain, */*',
            'Content-Type': 'application/json'
        }
    }
): Promise<IHttpResponse<T>> => {
    return await http<T>(new Request(path, args));
};

export const put = async <T>(
    path: string,
    body: any,
    args: RequestInit = { method: "put", body: JSON.stringify(body) }
): Promise<IHttpResponse<T>> => {
    return await http<T>(new Request(path, args));
};