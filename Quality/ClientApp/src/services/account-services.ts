import IResult from "../types/common";
import { post } from "./http";
import {ILoginDto} from "../types/account";

export const login = (data : ILoginDto) => {
    return new Promise<IResult>((resolve, reject) => {
        post<IResult>(
            "/api/user/login",
            {...data}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}

export const logout = () => {
    return new Promise<IResult>((resolve, reject) => {
        post<IResult>("/api/user/logout", {})
        .then(response => resolve(response.parsedBody))
        .catch(e => reject(e));
    });
}