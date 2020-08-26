import React, { Component } from "react";
import styled from "styled-components";

const Text = styled.body`
    font-family: Apercu, "Source Sans Pro", system, system-ui, -apple-system,
        BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
    line-height: 1.5;
`;
const Propic = styled.img`
    margin-left: 400px;
    width: 400px;
    height: 400px;
`;

export class Intro extends Component {
    render() {
        return (
            <Text>
                <section id="hero">
                    <div class="hero container">
                        <div>
                            <h1>
                                Hello, <span></span>
                            </h1>
                            <h1>
                                My Name is <span></span>
                            </h1>
                            <h1>
                                Daniel <span></span>
                            </h1>
                            <a href="#" type="button" class="cta">
                                About Me
                            </a>
                        </div>
                        <Propic src={require("./assets/img/Propic.png")} />
                    </div>
                </section>
            </Text>
        );
    }
}

export default Intro;
