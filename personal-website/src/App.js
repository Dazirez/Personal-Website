import React from "react";
import Intro from "./Intro";
import Background from "./Background";
class App extends React.Component {
    render() {
        return (
            <div style={{ width: '100%', margin: 'auto' }}>
                <Intro />
                <Background />
            </div>
        );
    }
}

export default App;
