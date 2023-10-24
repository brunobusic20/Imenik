import React, { Component } from "react";
import KontaktDataService from "../../services/kontakt.service";
import Container from 'react-bootstrap/Container';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Link } from "react-router-dom";




export default class DodajKontakt extends Component {

  constructor(props) {
    super(props);
    
    
    this.dodajKontakt = this.dodajKontakt.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async dodajKontakt(kontakt) {
    const odgovor = await KontaktDataService.post(kontakt);
    if(odgovor.ok){
      // routing na smjerovi
      window.location.href='/kontakti';
    }else{
      // pokaži grešku
     // console.log(odgovor.poruka.errors);
      let poruke = '';
      for (const key in odgovor.poruka.errors) {
        if (odgovor.poruka.errors.hasOwnProperty(key)) {
          poruke += `${odgovor.poruka.errors[key]}` + '\n';
         // console.log(`${key}: ${odgovor.poruka.errors[key]}`);
        }
      }

      alert(poruke);
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

    let prezime=0;
    if (podaci.get('prezime').trim().length>0){
     prezime =podaci.get('prezime')
    }

    this.dodajKontakt({
      ime: podaci.get('ime'),
      prezime: prezime,
      broj:podaci.get('broj'),
      adresa:podaci.get('adresa'),
      
    });
    
  }


  render() { 
    return (
    <Container>
        <Form onSubmit={this.handleSubmit}>


          <Form.Group className="mb-3" controlId="ime">
            <Form.Label>Ime</Form.Label>
            <Form.Control type="text" name="ime" placeholder="Ime osobe" maxLength={255} required/>
          </Form.Group>


          <Form.Group className="mb-3" controlId="prezime">
            <Form.Label>Prezime</Form.Label>
            <Form.Control type="text" name="prezime" placeholder="130" />
          </Form.Group>


          <Form.Group className="mb-3" controlId="broj">
            <Form.Label>Broj</Form.Label>
            <Form.Control type="text" name="broj" placeholder="500" />
            <Form.Text className="text-muted">
             Ne smije biti negativna
            </Form.Text>
          </Form.Group>

          <Form.Group className="mb-3" controlId="adresa">
            <Form.Label>Adresa</Form.Label>
            <Form.Control type="text" name="adresa" placeholder="50" />
          </Form.Group>

          

          <Row>
            <Col>
              <Link className="btn btn-danger gumb" to={`/kontakti`}>Odustani</Link>
            </Col>
            <Col>
            <Button variant="primary" className="gumb" type="submit">
              Dodaj kontakt
            </Button>
            </Col>
          </Row>
         
          
        </Form>


      
    </Container>
    );
  }
}

