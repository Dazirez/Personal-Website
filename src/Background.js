import React, { Component } from 'react';
import styled from 'styled-components';

const Back = styled.div`
  margin-top: 100px;
  width: 100vw;
  padding-left: 25%;
  padding-right: 25%;
  font-family: Apercu, 'Source Sans Pro', system, system-ui, -apple-system,
    BlinkMacSystemFont, Roboto, Helvetica, Arial, sans-serif;
`;
const Paragraph = styled.p`
  margin-bottom: 25px;
`;

const Content = styled.div`
  margin-left: 75px;
  display: flex;
  flex-direction: column;
  justify-content: center;
`;

export class Background extends Component {
  render() {
    return (
      <Back>
        <div class='wrapper'>
          <h4 style={{ color: '#007bff' }}> Background </h4>
          <Content>
            <Paragraph>
              <span id='caption'>Coder, Dreamer, Musician</span>
            </Paragraph>
            <Paragraph>
              I'm currently a Student at the University of Michigan studying
              Computer Science and Japanese.
            </Paragraph>
            <Paragraph>
              As a detail-oriented software engineer- my passion is for making a
              great user experience work with novel technology. I have a keen
              eye for aesthetics, and am fascinated by good design. I'm
              hardworking and always excited about crazy ideas.
            </Paragraph>
            <Paragraph>
              I've played for my Collegiate League of Legends team and have attended and won varying competitions. When I'm not playing tennis or music, I also enjoy
              exploring a variety of new subjects.
            </Paragraph>
          </Content>
        </div>
      </Back>
    );
  }
}

export default Background;
