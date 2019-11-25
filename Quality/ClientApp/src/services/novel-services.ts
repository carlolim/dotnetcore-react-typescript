import { INovelDto, INewNovelDto } from "../types/novel";
import { get, post } from "./http";
import IResult from "../types/common";

export const allNovels = () => {
    return new Promise<INovelDto[]>((resolve, reject) => {
        get<INovelDto[]>(
            "/api/novel",
            {}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}

export const addNovel = (data: INewNovelDto) => {
    return new Promise<IResult>((resolve, reject) => {
        post<IResult>(
            "/api/novel",
            {...data}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}