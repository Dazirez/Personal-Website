import React from "react";
import Intro from "./Intro";
import Stream from "./Stream";
import Background from "./Background";
import Skills from "./Skills";
import Footer from "./Footer";
class App extends React.Component {
    render() {
        return (
            <body>
                <Intro />
                <Stream />
                <Background />
                <Skills />
                <Background />
                <Footer />
            </body>
        );
    }
}

export default App;
