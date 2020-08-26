import React from "react";
import Intro from "./Intro";
import Stream from "./Stream";
import Background from "./Background";
import Skills from "./Skills";
import Footer from "./Footer";
import "./assets/css/style.css"
class App extends React.Component {
    render() {
        return (
            <body>
                <Intro />
                <Background />
                <Skills />
                <Footer />
            </body>
        );
    }
}

export default App;
