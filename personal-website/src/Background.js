import React, { Component } from "react";
import styled from "styled-components";



const Introduction = styled.div`
    width: 100%;
    padding: 120px 100px; 
    display: flex; 
    justify-content: space-around; 
    max-width: 1440px; 
    height: 100vh;
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
`;
const Hello = styled.h1`
    display: block;
    margin-block-start: 0.67em;
    margin-block-end: 0.67em;
    margin-inline-start: 0px;
    margin-inline-end: 0px;
`;
const Text = styled.body`
    font-family: Apercu,"Source Sans Pro",system,system-ui,-apple-system,BlinkMacSystemFont,Roboto,Helvetica,Arial,sans-serif;
    line-height: 1.5;
`;

export class Background extends Component {
    render() {
        return (
            <Introduction>
                <Text>
                    <Hello>Hello</Hello>
                    <h2>Prospective Software Developer from Michigan. I’m currently studying computer science at the University of Michigan- Ann Arbor.

                    I love making music and experiencing new things. I play the guitar and piano, and am seriously curious about anything that comes my way.

                    If you’d like to make something happen, feel free to get in touch with me 🙂</h2>
                    <h3>Get in Touch</h3>
                </Text>
            </Introduction>
        );
    }
}

export default Background;
