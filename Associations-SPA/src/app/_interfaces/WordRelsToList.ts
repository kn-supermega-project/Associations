import { Words } from "./Words";


export interface WordRelsToList {
    id: number;
    wordId: number;
    wordRelId: number;
    malePoints: number;
    femalePoints: number;
    totalPoints: number;
    malePercents: number;
    femalePercents: number;
    totalPercents: number;
    relWord: Words;
}
