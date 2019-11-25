import { get, post } from "./http";
import IResult from "../types/common";
import { IGenreDto, INewGenreDto } from "../types/genre";

export const allGenres = () => {
    return new Promise<IGenreDto[]>((resolve, reject) => {
        get<IGenreDto[]>(
            "/api/genre",
            {}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}

export const addGenre = (data: INewGenreDto) => {
    return new Promise<IResult>((resolve, reject) => {
        post<IResult>(
            "/api/genre",
            {...data}
        ).then(response => resolve(response.parsedBody))
        .catch(e => {
            reject(e);
        })
    });
}