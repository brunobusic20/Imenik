import http from "../http-common";


class KontaktDataService{

    async get(){
        return await http.get('/Kontakt');
    }

    async getBySifra(sifra) {
        return await http.get('/kontakt/' + sifra);
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


    async post(kontakt){
        //console.log(smjer);
        const odgovor = await http.post('/kontakt',kontakt)
           .then(response => {
             return {ok:true, poruka: 'Unio kontakt'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
    }

    async put(sifra,kontakt){
        //console.log(smjer);
        const odgovor = await http.put('/kontakt/' + sifra,kontakt)
           .then(response => {
             return {ok:true, poruka: 'Promjenio kontakt'}; // return u odgovor
           })
           .catch(error => {
            //console.log(error.response);
             return {ok:false, poruka: error.response.data}; // return u odgovor
           });
     
           return odgovor;
         }
        
}
export default new KontaktDataService();

