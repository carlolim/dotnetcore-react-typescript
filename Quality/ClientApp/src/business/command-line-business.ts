import { ILoginDto } from "../types/account";
import { login, logout } from "../services/account-services";
import { allNovels, addNovel } from "../services/novel-services";
import { INewNovelDto } from "../types/novel";
import { INewGenreDto } from "../types/genre";
import { addGenre, allGenres } from "../services/genre-services";
import { INewChapterDto } from "../types/chapter";
import { byNovelId, addChapter } from "../services/chapter-services";

export const listNovelsCommand = async () : Promise<string[]> => {
    var result: string[] = [];
    var novels = await allNovels();
    novels.forEach(novel => {
        result.push(`> Title: ${novel.title} | Author: ${novel.author}`);
    });
    if (result.length === 0) result.push("> No novels in the database");
    return result;
}

export const addNovelCommand = async (cmd: string) : Promise<string[]> => {
    var result: string[] = [];
    try {
        var cmdParts = cmd.split(' "');
        var novel: INewNovelDto = {
            author: cmdParts[2].replace('"', ''),
            title: cmdParts[1].replace('"', ''),
            description: cmdParts[3].replace('"', ''),
        }
        var r = await addNovel(novel);
        result.push(r.message)
    }
    catch (e) {
        var message: string;
        if (e.status === 403) message = "> Error: No permissions to perform this action"
        else message = '> Invalid command, please follow this format [create novel "title" "author" "description"]';
        result.push(message);
    }
    return result;
}

export const loginCommand = async (cmd: string): Promise<string[]> => {
    var result: string[] = [];
    try {
        var cmdParts = cmd.split(' "');
        var credential: ILoginDto = {
            password: cmdParts[2].replace('"', ''),
            username: cmdParts[1].replace('"', ''),
        }
        var loginResult = await login(credential);
        result.push(loginResult.message);
    } catch (e) {
        result.push('> Invalid username or password');
    }
    return result;
}

export const logoutCommand = async () : Promise<string[]> => {
    var result : string[] = [];
    try {
        var logoutResult = await logout();
        result.push(logoutResult.message);
    } catch (e) {
        result.push('> An error occured while logging out :(');
    }
    return result;
}

export const addGenreCommand = async (cmd: string) : Promise<string[]> => {
    var result: string[] = [];
    try {
        var cmdParts = cmd.split(' "');
        var data: INewGenreDto = {
            name: cmdParts[1].replace('"', ''),
        }
        var r = await addGenre(data);
        result.push(r.message)
    }
    catch (e) {
        var message: string;
        if (e.status === 403) message = "> Error: No permissions to perform this action"
        else message = '> Invalid command, please follow this format [create genre "name"]';
        result.push(message);
    }
    return result;
}

export const listGenresCommand = async () : Promise<string[]> => {
    var result: string[] = [];
    var genre = await allGenres();
    genre.forEach(g => {
        result.push(`> Id: ${g.genreId} | name: ${g.name}`);
    });
    if (result.length === 0) result.push("> No genre in the database");
    return result;
}

export const listChapterCommand = async (cmd: string) : Promise<string[]> => {
    var result: string[] = [];
    try{
        cmd = cmd.replace(' ', '');
        cmd = cmd.replace('listchapters', '');
        var novelId: number = Number(cmd);
        var chapters = await byNovelId(novelId);
        if (chapters.length === 0) result.push("> No chapter for this novel in the database");
        chapters.forEach(c => {
            result.push(`> Chapter ${c.chapterNumber}: ${c.title} (${(c.isPublished ? 'published' : 'not published')})`);
        });
    }catch {
        result.push('> Invalid command, please follow this format [list chapters NovelId]');
    }
    return result;
}

export const addChapterCommand = async (cmd:string) : Promise<string[]> => {
    var result: string[] = [];
    try {
        var cmdParts = cmd.split(' "');
        var data: INewChapterDto = {
            chapterNumber: Number(cmdParts[2].replace('"', '')),
            contents: cmdParts[4].replace('"', ''),
            isPublished: false,
            novelId: Number(cmdParts[1].replace('"', '')),
            title: cmdParts[3].replace('"', '')
        }
        console.log(cmdParts, data);
        var r = await addChapter(data);
        result.push(r.message)
    }
    catch (e) {
        var message: string;
        if (e.status === 403) message = "> Error: No permissions to perform this action"
        else message = '> Invalid command, please follow this format [create chapter "NovelId", "ChapterNumber", "Title", "Content"]';
        result.push(message);
    }
    return result;
}