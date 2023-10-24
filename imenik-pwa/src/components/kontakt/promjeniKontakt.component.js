import React, { Component } from "react";
import KontaktDataService from "../../services/kontakt.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";



export default class PromjeniKontakt extends Component {

  constructor(props) {
    super(props);
    

   
    this.kontakt = this.dohvatiKontakt();
    this.promjeniKontakt = this.promjeniKontakt.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    

    this.state = {
      kontakt: {}
    };

  }



  async dohvatiKontakt() {
    let href = window.location.href;
    let niz = href.split('/'); 
    await KontaktDataService.getBySifra(niz[niz.length-1])
      .then(response => {
        this.setState({
          kontakt: response.data
        });
       // console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
    
   
  }

  async promjeniKontakt(kontakt) {
    // ovo mora bolje
    let href = window.location.href;
    let niz = href.split('/'); 
    const odgovor = await KontaktDataService.put(niz[niz.length-1],kontakt);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/kontakti';
    }else{
      // pokaži grešku
      console.log(odgovor);
    }
  }



  handleSubmit(e) {
    // Prevent the browser from reloading the page
    e.preventDefault();

    // Read the form data
    const podaci = new FormData(e.target);
    //Object.keys(formData).forEach(fieldName => {
    // console.log(fieldName, formData[fieldName]);
    //})
    
    //console.log(podaci.get('verificiran'));
    // You can pass formData as a service body directly:

    this.promjeniKontakt({
      ime: podaci.get('ime'),
      prezime:podaci.get('prezime'),
      broj: podaci.get('broj'),
      adresa: podaci.get('adresa'),
      
    });
    
  }


  render() {
    
   const { kontakt} = this.state;


    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Ime osobe"
            maxLength={255} defaultValue={kontakt.ime} required />
          </Form.Group>


          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" defaultValue={kontakt.prezime}  placeholder="130" />
          </Form.Group>


          <Form.Group className="mb-3" controlId="broj">
            <Form.Label>Broj</Form.Label>
            <Form.Control type="text" name="broj" defaultValue={kontakt.broj}  placeholder="500" />
          </Form.Group>

          <Form.Group className="mb-3" controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" defaultValue={kontakt.adresa}  placeholder="50" />
          </Form.Group>

          

        
         
          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/kontakti`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Promjeni kontakt
            </Button>
            </Col>
          </Row>
        </Form>


      
    </Container>
    );
  }
}

