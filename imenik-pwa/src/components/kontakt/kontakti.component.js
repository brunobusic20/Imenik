import React, { Component } from "react";
import { Button, Container, Table } from "react-bootstrap";
import KontaktDataService from "../../services/kontakt.service";
import { Link } from "react-router-dom";
import {FaEdit, FaTrash} from "react-icons/fa"


export default class Kontakti extends Component{

    constructor(props){
        super(props);

        this.state = {
            kontakti: []
        };

    }

    componentDidMount(){
        this.dohvatiKontakti();
    }

    async dohvatiKontakti(){

        await KontaktDataService.get()
        .then(response => {
            this.setState({
                kontakti: response.data
            });
            console.log(response.data);
        })
        .catch(e =>{
            console.log(e);
        });
    }

    async obrisiKontakt(sifra){
        const odgovor = await KontaktDataService.delete(sifra);
        if(odgovor.ok){
            this.dohvatiKontakti();
        }else{
            alert(odgovor.poruka);
        }
    }


    render(){

        const { kontakti } = this.state;

        return (
            <Container>
               <a href="/kontakti/dodaj" className="btn btn-success gumb">
                Dodaj novi kontakt
               </a>
                
               <Table striped bordered hover responsive>
                <thead>
                    <tr>
                        <th>Ime</th>
                        <th>Prezime</th>
                        <th>Broj</th>
                        <th>Adresa</th>
                       
                    </tr>
                </thead>
                <tbody>
                   { kontakti && kontakti.map((kontakt,index) => (

                    <tr key={index}>
                        <td>{kontakt.ime}</td>
                        <td className="prezime">{kontakt.prezime}</td>
                        <td className="broj">{kontakt.broj}
                            
                               
                        </td>
                        <td className="sredina">{kontakt.adresa}</td>
                        <td>
                            <Link className="btn btn-primary gumb"
                            to={`/kontakti/${kontakt.sifra}`}>
                                <FaEdit />
                            </Link>

                            <Button variant="danger" className="gumb"
                            onClick={()=>this.obrisiKontakt(kontakt.sifra)}>
                                <FaTrash />
                            </Button>
                        </td>
                    </tr>

                   ))}
                </tbody>
               </Table>



            </Container>


        );
    }
}