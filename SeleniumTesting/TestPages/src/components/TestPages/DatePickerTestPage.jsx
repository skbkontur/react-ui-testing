import React from 'react';
import Button from 'retail-ui/components/Button'
import DatePicker from 'retail-ui/components/DatePicker'
import { CaseSuite, Case } from '../Case';

export default class DatePickerTestPage extends React.Component {
    state = {
        simpleSelect1: null,
        select2: null,
    };

    render(): React.Element<*> {
        return (
            <CaseSuite title='Дейтпикеры'>
                <Case title='Простой дейтпикер'>
                    <Case.Body>
                        <DatePicker
                            data-tid='SimpleDatePicker'
                            value={this.state.value} 
                            onChange={(e, value) => this.setState({ value: value })}
                        />
                    </Case.Body>
                </Case>
           </CaseSuite>
        );
    }
}