export interface IChapterDto {
    novelChapterId: number,
    novelId: number,
    title: string,
    chapterNumber: number,
    contents: string,
    isPublished: boolean
}

export interface INewChapterDto {
    novelId: number,
    title: string,
    chapterNumber: number,
    contents: string,
    isPublished: boolean
}
export default {};