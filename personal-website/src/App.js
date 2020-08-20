import React from "react";
import Intro from "./Intro";
import Background from "./Background";
import Skills from "./Skills";
import Footer from "./Footer";
class App extends React.Component {
    render() {
        return (
            <div style={{ width: '100%', margin: 'auto' }}>
                <Intro />
                <Background />
                <Skills />
                <Background />
                <Footer />
            </div>
        );
    }
}

export default App;
