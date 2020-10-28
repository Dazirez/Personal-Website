import React from "react";
import Intro from "./Intro";
import Stream from "./Stream";
import Projects from "./Projects";
import Background from "./Background";
import Skills from "./Skills";
import Footer from "./Footer";
class Home extends React.Component {
    render() {
        return (
            <body>
                <Intro />
                <Stream />
                <Background />
                <Skills />
                <Projects />
                <Footer />
            </body>
        );
    }
}

export default Home;
