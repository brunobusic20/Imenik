import http from "../http-common";

class kontaktDataService{
    async get(){
        return await http.get('Kontakt')
    }
}

export default new kontaktDataService();