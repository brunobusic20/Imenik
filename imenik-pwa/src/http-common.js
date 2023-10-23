import axios from "axios";


export default axios.create({
    baseURL: "https://brunobusic20-001-site1.gtempurl.com/",
    headers: {
        "Content-Type": "application/json"
    }
});