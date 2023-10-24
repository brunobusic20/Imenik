import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route  } from 'react-router-dom';
import Izbornik from './components/izbornik.component';
import Pocetna from './components/pocetna.component';
import NadzornaPloca from './components/nadzornaploca.component';
import Kontakti from './components/kontakt/kontakti.component';
import DodajKontakt from './components/kontakt/dodajKontakt.component';
import PromjeniKontakt from './components/kontakt/promjeniKontakt.component';


export default function App() {
  return (
    <Router>
      <Izbornik />
      <Routes>
        <Route path='/' element={<Pocetna />} />
        <Route path='/nadzornaploca' element={<NadzornaPloca />} />
        <Route path='/kontakti' element={<Kontakti />} />
        <Route path='/kontakti/dodaj' element={<DodajKontakt />} />
        <Route path='/kontakti/:sifra' element={<PromjeniKontakt />} />
      </Routes>
     
    </Router>
  );
}
