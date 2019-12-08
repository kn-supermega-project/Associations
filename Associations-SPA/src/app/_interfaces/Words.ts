
import { WordRelsToList } from "./WordRelsToList";

export interface Words {
    id: number;
    word: string;
    type: number;
    rels: WordRelsToList[];
}
