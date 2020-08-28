import React, { Component } from "react";
import styled from "styled-components";

const Back = styled.div`
    margin: 100px 0px;
    display: flex;
    flex-direction: row;
    justify-content: center;
    font-family: Apercu, "Source Sans Pro", system, system-ui, -apple-system,
        BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
    flex-wrap: wrap;
`;
const Paragraph = styled.p`
    margin-bottom: 25px;
    min-width: 300px; 
`;

const Content = styled.div`
    flex-basis: 25%; 
    padding-left: 100px;
    display: flex;
    max-width: 50%;
    flex-direction: column;
    justify-content: center;
`;

export class Background extends Component {
    render() {
        return (
            <Back>
                <h4 style={{ color: "#007bff" }}> Background </h4>
                <Content>
                    <Paragraph>
                        I'm currently a Student at the University of Michigan
                        studying Computer Science and Japanese.
                    </Paragraph>
                    <Paragraph>
                        As a design-oriented software engineer- my passion is
                        for making a great user experience work with novel
                        technology. I have a keen eye for aesthetics, and am
                        fascinated by good design. I'm hardworking and always
                        excited about crazy ideas.
                    </Paragraph>
                    <Paragraph>
                        I've played for my Collegiate League of Legends team and
                        won many competitions. When I'm not playing tennis or
                        music, I also enjoy exploring a variety of new subjects.
                    </Paragraph>
                </Content>
            </Back>
        );
    }
}

export default Background;
