import { Palestrante } from './Palestrante';
import { Lote } from './Lote';
import { RedeSocial } from './RedeSocial';
export interface Evento {
    id: number;
    local: string;
    tema: string;
    dataEvento: Date;
    qtdPessoas: number;
    imagemurl: string;
    telefone: string;
    email: string;
    lotes: Lote[];
    redesSociais: RedeSocial[];
    palestrante: Palestrante[];

}
