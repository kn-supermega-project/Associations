import { WordsRel } from "./WordsRel";

export interface Words {
    id: number;
    word: string;
    rels: WordsRel[];
}
