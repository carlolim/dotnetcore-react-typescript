import { IChapterDto, INewChapterDto } from "../types/chapter";
import { get, post } from "./http";
import IResult from "../types/common";

export const byNovelId = (id: number) => {
    return new Promise<IChapterDto[]>((resolve, reject) => {
        get<IChapterDto[]>(
            `/api/novel/${id}/chapters`
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}

export const addChapter = (data: INewChapterDto) => {
    return new Promise<IResult>((resolve, reject) => {
        post<IResult>(
            `/api/chapter`,
            {...data}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    })
}