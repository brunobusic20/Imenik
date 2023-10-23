import http from "../http-common";


class KontaktDataService{

    async get(){
        return await http.get('/Kontakt');
    }

    async delete(sifra){
        const odgovor = await http.delete('/Kontakt/' + sifra)
        .then(response => {
            return {ok: true, poruka: 'Obrisao uspješno'};
        })
        .catch(e=>{
            return {ok: false, poruka: e.response.data};
        });

        return odgovor;
    }

}

export default new KontaktDataService();